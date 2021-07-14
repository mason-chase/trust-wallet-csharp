using System.Collections.Generic;
using System.Text;
using Mc2.TrustWallet.Asset.ModelsStandard;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.Utilities
{
    /// <summary>
    /// C# Static Data Code Generator 
    /// </summary>
    public static class AssetCodeGenerator
    {
        /// <summary>
        /// Generate a constant string to update static code data. 
        /// </summary>
        /// <param name="assetStrings"></param>
        /// <returns></returns>
        public static (string, string) GenAssetsSymbols(IList<IAsset> assetStrings)
        {
            StringBuilder assetsSymbols = new();
            assetsSymbols.Append(GetAssetsSymbolsHeader());

            StringBuilder assetsDict = new();
            assetsDict.Append(GetAssetDictHeader());

            foreach (IAsset asset in assetStrings)
            {
                string constAsset = asset.Symbol.Code.ToConstantCase(true) + "_" + asset.Name.ToConstantCase();
                assetsSymbols.Append(GetAssetsSymbolsConsts(asset.Name, ref constAsset));
                assetsDict.Append(GetAssetDict(asset, ref constAsset));
            }

            assetsSymbols.Append(GetAssetsSymbolsFooter());
            assetsDict.Append(GetAssetDictFooter());

            return (assetsSymbols.ToString(), assetsDict.ToString());
        }

        /// <summary>
        /// Refer toAssetsSymbols Sample
        /// </summary>
        /// <returns></returns>
        private static string GetAssetsSymbolsHeader()
        {
            return @"namespace Mc2.TrustWallet.Asset.Data
{
    public static class AssetsConsts
    {
";
        }

        private static string GetAssetsSymbolsConsts(string name, ref string constAsset)
        {
            
            return  "        /// <summary>\n" +
                    "        /// " + name + "\n"+
                    "        /// </summary>\n" +
                   @$"        public const string {constAsset} = ""{constAsset}"";"+"\n\n";
        }

        private static string GetAssetsSymbolsFooter()
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
using Mc2.TrustWallet.Asset.ModelsStandard;
using Mc2.TrustWallet.Asset.ModelsStandard.AssetProperties;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.Data
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


        private static string GetAssetDict(IAsset asset, ref string constAsset)
        {
            StringBuilder assetDictItem = new();
            assetDictItem.Append(@$"
            assets.Add(
                AssetsConsts.{constAsset},
                new Coin
                {{
                    Name = @""{asset.Name.EscapeQuotes()}"",
                    Code = ""{asset.Symbol}"",
                    Symbol = AssetSymbol.FromString( AssetsConsts.{constAsset}),
                    Website = " + asset.Website.ToCodeString() + @$",
                    Description = @""{asset.Description.EscapeQuotes() }"",
                    ShortDescription = ""{asset.ShortDescription}"",
                    Explorer = " + asset.Explorer.ToCodeString() + @$",
                    Research = " + asset.Research.ToCodeString() + @$",
                    Decimals = {asset.Decimals},
                    Status = AssetStatus.{asset.Status},
                    LogoPng = File.ReadAllBytes(AssetsConsts.{constAsset}+ "".png""),");
            if (asset.Links.Length > 0)
            {
                assetDictItem.Append(@"
                    Links = new Link[] {
");

                StringBuilder socialSb = new();
                foreach (Link social in asset.Links)
                {
                    socialSb.Append(@$"
                        new Link {{
                            Name = ""{social.Name}"",
                            Url = new Uri(""{social.Url}"")
                        }},");
                }
                assetDictItem.Append(socialSb.ToString().TrimEnd(new char[] { ',' }));
                assetDictItem.Append(@"
                    },");
            }
            else
                assetDictItem.Append(@"
                    Links = Array.Empty<Link>(),");

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
