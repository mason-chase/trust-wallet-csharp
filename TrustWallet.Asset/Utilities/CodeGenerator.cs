using System.Collections.Generic;
using System.Text;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.Interfaces;

namespace TrustWallet.Asset.Utilities
{
    /// <summary>
    /// C# Static Data Code Generator 
    /// </summary>
    public static class CodeGenerator
    {
        /// <summary>
        /// Generate a constant string to update static code data. 
        /// </summary>
        /// <param name="assetStrings"></param>
        /// <returns></returns>
        public static (string, string) GenAssetSymbols(IList<IAssetString> assetStrings)
        {
            StringBuilder assetSymbols = new();
            assetSymbols.Append(GetAssetSymbolsHeader());

            StringBuilder assetsDict = new();
            assetsDict.Append(GetAssetDictHeader());

            foreach (IAssetString asset in assetStrings)
            {
                string constAsset = asset.Symbol.ToConstantCase(true) + "_" + asset.Name.ToConstantCase();
                assetSymbols.Append(GetAssetSymbolsConsts(asset.Name, ref constAsset));
                assetsDict.Append(GetAssetDict(asset, ref constAsset));
            }

            assetSymbols.Append(GetAssetSymbolsFooter());
            assetsDict.Append(GetAssetDictFooter());

            return (assetSymbols.ToString(), assetsDict.ToString());
        }

        /// <summary>
        /// Refer toAssetSymbols Sample
        /// </summary>
        /// <returns></returns>
        private static string GetAssetSymbolsHeader()
        {
            return @"namespace TrustWallet.Asset.Data
{
    public static class AssetSymbols
    {
";
        }

        private static string GetAssetSymbolsConsts(string name, ref string constAsset)
        {
            
            return  "        /// <summary>\n" +
                    "        /// " + name + "\n"+
                    "        /// </summary>\n" +
                   @$"        public const string {constAsset} = ""{constAsset}"";"+"\n\n";
        }

        private static string GetAssetSymbolsFooter()
        {
            return @"
    }
}
";
        }

        /// <summary>
        /// Refer to <see cref="AssetsDictSample" />
        /// </summary>
        /// <returns></returns>
        private static string GetAssetDictHeader()
        {
            return @"using System;
using System.IO;
using System.Collections.Generic;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.Data
{
    public static partial class Assets
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<string, IAsset> GetAssets()
        {
            var assets = new Dictionary<string, IAsset>();
            #region DictData";
            
        }


        private static string GetAssetDict(IAssetString asset, ref string constAsset)
        {
            StringBuilder assetDictItem = new();
            assetDictItem.Append(@$"
            assets.Add(
                AssetSymbols.{constAsset},
                new Coin
                {{
                    Name = @""{asset.Name.EscapeQuotes()}"",
                    Code = ""{asset.Symbol}"",
                    Symbol = AssetSymbol.FromString( AssetSymbols.{constAsset}),
                    Website = " + asset.Website.ToCodeString() + @$",
                    SourceCode = " + asset.SourceCode.ToCodeString() + @$",
                    WhitePaper = " + asset.WhitePaper.ToCodeString() + @$",
                    Description = @""{asset.Description.EscapeQuotes() }"",
                    ShortDescription = ""{asset.ShortDescription}"",
                    Explorer = " + asset.Explorer.ToCodeString() + @$",
                    Research = " + asset.Research.ToCodeString() + @$",
                    Decimals = {asset.Decimals},
                    Status = AssetStatus.{asset.Status},
                    LogoPng = File.ReadAllBytes(AssetSymbols.{constAsset}+ "".png""),");
            if (asset.Socials.Length > 0)
            {
                assetDictItem.Append(@"
                    Socials = new Social[] {
");

                StringBuilder socialSb = new();
                foreach (Social social in asset.Socials)
                {
                    socialSb.Append(@$"
                        new Social {{
                            Name = ""{social.Name}"",
                            Url = new Uri(""{social.Url}""),
                            Handle = ""{social.Handle}""
                        }},");
                }
                assetDictItem.Append(socialSb.ToString().TrimEnd(new char[] { ',' }));
                assetDictItem.Append(@"
                    },");
            }
            else
                assetDictItem.Append(@"
                    Socials = Array.Empty<Social>(),");

            // trim trailing comma and append to code
            if (asset.Tags.Length > 0)
            {
                assetDictItem.Append(@"
                    Tags = new string[] { """ + (asset.Tags is null ? "" : string.Join(@""", """, asset.Tags)) + @"""}");
            }
            else
                assetDictItem.Append(@"
                    Tags = Array.Empty<string>()");

            assetDictItem.Append(@"
                });");

            return assetDictItem.ToString();

        }

        /// <summary>
        /// Footer for for AssetDict <see cref="AssetsDictSample" />
        /// </summary>
        /// <returns></returns>
        private static string GetAssetDictFooter()
        {
            return @"
            #endregion
            return assets;
        }
    }
}
";
        }
    }
}
