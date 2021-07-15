using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mc2.TrustWallet.Asset.FolderModels;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;
using Mc2.TrustWallet.Asset.Services.CodeSignatureServiceProperties;
using Mc2.TrustWallet.Asset.Utilities;
using Microsoft.CodeAnalysis;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Services
{
    public class MigrationService
    {
        private readonly ILogger _logger;

        public MigrationService(ILogger<MigrationService> logger)
        {
            _logger = logger;
        }

        public void RebuildFromFolder()
        {

        }

        public void Rebuild()
        {
            IDictionary<string, BlockchainFolder> blockchains;
            (blockchains, _) = TwFolderToolGlobalData.GetBlockChainFolder();

            IDictionary<string, IAsset> assets = TwFolderToolGlobalData.GetAllAssetsString(blockchains);

            _logger.LogDebug(string.Format("Parsed {0} blockchain", blockchains.Count));


            (string assetSymbolsClass, string assetsDict) = AssetCodeGenerator.GenAssetsSymbols(assets.Values.ToList());

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

            if (assets.ToString().GetSHA256() != signature.Assets
                || assetSymbolSha256 != signature.AssetSymbolConstsCs
                || assetsDictSha256 != signature.AssetsDictCs)
            {
                _logger.LogInformation("AssetSymbol needs to update");
                CodeSignatureService.SaveSignatures(new CodeSignatures
                {
                    Assets = assets.ToString().GetSHA256(),
                    AssetSymbolConstsCs = assetSymbolSha256,
                    AssetsDictCs = assetsDictSha256
                });

                CodeSignatureService.SaveAssetsJson(assets);

                File.WriteAllText(AssetConstsCsPath, assetSymbolsClass);
                File.WriteAllText(AssetsDictCsPath, assetsDict);
            }


            AssetLogoService.Persist(ref assets);

        }
    }
}
