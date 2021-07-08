using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrustWallet.Asset.Utilities;
using Microsoft.CodeAnalysis;
using TrustWallet.Asset.ModelsFolder;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using static TrustWallet.Asset.Data.Settings;
using TrustWallet.Asset.Services.CodeSignatureServiceProperties;

namespace TrustWallet.Asset.Services
{
    public class MigrationService
    {
        private readonly ILogger _logger;

        public MigrationService(ILogger<MigrationService> logger)
        {
            _logger = logger;
        }
        public void Rebuild()
        {
            IDictionary<string, BlockchainFolder> blockchains;
            (blockchains, _) = TwFolderTool.GetBlockChainFolder();

            IDictionary<string, IAssetString> assets = TwFolderTool.GetAllAssetsString(blockchains);

            _logger.LogDebug(string.Format("Parsed {0} blockchain", blockchains.Count));


            (string assetSymbolsClass, string assetsDict) = CodeGenerator.GenAssetsSymbols(assets.Values.ToList());

            CodeSignatures signature = CodeSignatureService.GetSignatures();


            string assetSymbolSha256 = assetSymbolsClass.GetSHA256();
            string assetsDictSha256 = assetsDict.GetSHA256();


            ClassDeclarationSyntax newClassNode = SyntaxFactory.ParseSyntaxTree(assetSymbolsClass).GetRoot()
                                            .DescendantNodes()
                                            .OfType<ClassDeclarationSyntax>()
                                            .FirstOrDefault();

            // Retrieve the parent namespace declaration
            NamespaceDeclarationSyntax parentNamespace = (NamespaceDeclarationSyntax)newClassNode?.Parent;

            // Add the new class to the namespace
            parentNamespace?.AddMembers(newClassNode).NormalizeWhitespace();

            if (assetSymbolSha256 != signature.AssetsConstsCs)
            {
                _logger.LogInformation("AssetSymbol needs to update");
                CodeSignatureService.SaveSignatures(new CodeSignatures
                {
                    AssetsConstsCs = assetSymbolSha256,
                    AssetsDictCs = assetsDictSha256
                });

                File.WriteAllText(AssetSymbolsPath, assetSymbolsClass);
            }

            if (assetsDictSha256 != signature.AssetsDictCs)
            {
                _logger.LogInformation("AssetsDict needs to update");
                CodeSignatureService.SaveSignatures(new CodeSignatures
                {
                    AssetsConstsCs = assetSymbolSha256,
                    AssetsDictCs = assetsDictSha256
                });
                File.WriteAllText(AssetsDictPath, assetsDict);
            }

            AssetLogoService.Persist(ref assets);

        }
    }
}
