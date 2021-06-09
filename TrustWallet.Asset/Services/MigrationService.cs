using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrustWallet.Asset.FolderModels;
using TrustWallet.Asset.Utilities;
using Microsoft.CodeAnalysis;
using static TrustWallet.Asset.Data.Settings;
using TrustWallet.Asset.Services.CodeSignatureServiceProperties;

namespace TrustWallet.Asset.Services
{
    public class MigrationService
    {
        private ILogger _logger;

        public MigrationService(ILogger<MigrationService> logger)
        {
            _logger = logger;
        }
        public void Rebuild()
        {
            IDictionary<string, BlockchainFolder> blockchains;
            IDictionary<string, BlockchainFolder> blockchainsPending;
            (blockchains, blockchainsPending) = TwFolderTool.GetBlockChainFolder();

            IDictionary<string, IAssetString> assets = TwFolderTool.GetAllAssetsString(blockchains);

            var assetString = assets.Select(x => x.Value.Name).ToArray();
            

            //foreach (var asset in assets)
            //{
            //    string assetJson = JsonConvert.SerializeObject(asset);
            //    File.WriteAllText($"..{DS}..{DS}..{DS}..{DS}Data{DS}{asset.Key}.json", assetJson);
            //}



            _logger.LogDebug($"Parsed {blockchains.Count} blockchain");


            (string assetSymbolsClass, string assetsDict) = CodeGenerator.GenAssetSymbols(assets.Values.ToList());

            CodeSignatures signature = CodeSignatureService.GetSignatures();


            string assetSymbolSha256 = assetSymbolsClass.GetSHA256();
            string assetsDictSha256 = assetsDict.GetSHA256();

            if (assetSymbolSha256 != signature.AssetConstsCs)
            {
                _logger.LogInformation("AssetSymbol needs to update");
                CodeSignatureService.SaveSignatures(new CodeSignatures
                {
                    AssetConstsCs = assetSymbolSha256,
                    AssetsDictCs = assetsDictSha256
                });

                File.WriteAllText(AssetSymbolsPath, assetSymbolsClass);
            }

            if (assetsDictSha256 != signature.AssetsDictCs)
            {
                _logger.LogInformation("AssetsDict needs to update");
                CodeSignatureService.SaveSignatures(new CodeSignatures
                {
                    AssetConstsCs = assetSymbolSha256,
                    AssetsDictCs = assetsDictSha256
                });

                File.WriteAllText(AssetsDictPath, assetsDict);
            }

            AssetImages.Persist(ref assets);

            var newClassNode = SyntaxFactory.ParseSyntaxTree(assetSymbolsClass).GetRoot()
                                            .DescendantNodes()
                                            .OfType<ClassDeclarationSyntax>()
                                            .FirstOrDefault();
            //// Retrieve the parent namespace declaration
            var parentNamespace = (NamespaceDeclarationSyntax) newClassNode.Parent;
            // Add the new class to the namespace
            var newParentNamespace = parentNamespace.AddMembers(newClassNode).NormalizeWhitespace();

            
            

            //OutputHelper.WriteLine(blockchains.Select(x => x.Value).ToArray().ToString());
        }
    }
}
