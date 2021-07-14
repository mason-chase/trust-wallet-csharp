using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.Utilities
{
    /// <summary>
    /// C# Static Data Code Generator 
    /// </summary>
    public static class BlockchainCodeGenerator
    {
        /// <summary>
        /// Generate a constant string to update static code data. 
        /// </summary>
        /// <param name="blockchains"></param>
        /// <returns></returns>
        public static (string, string) GenAssetsSymbols(IList<IBlockchain> blockchains)
        {
            StringBuilder assetsSymbols = new();
            assetsSymbols.Append(GetAssetsSymbolsHeader());

            StringBuilder assetsDict = new();
            assetsDict.Append(GetDictHeader());

            foreach (IBlockchain blockchain in blockchains)
            {
                string constAsset = blockchain.Code.ToConstantCase(true);
                assetsSymbols.Append(GetConsts(blockchain.Name, ref constAsset));
                assetsDict.Append(GetDictItems(blockchain, ref constAsset));
            }

            assetsSymbols.Append(GetAssetsSymbolsFooter());
            assetsDict.Append(GetAssetDictFooter());

            return (assetsSymbols.ToString(), assetsDict.ToString());
        }

        /// <summary>
        /// Refer to AssetsSymbolConsts Sample
        /// </summary>
        /// <returns></returns>
        private static string GetAssetsSymbolsHeader()
        {
            return @"namespace Mc2.TrustWallet.Asset.ModelsStandard
{
    public static class AssetSymbolConsts
    {
";
        }

        private static string GetConsts(string name, ref string constName)
        {

            return "        /// <summary>\n" +
                    "        /// " + name + "\n" +
                    "        /// </summary>\n" +
                   @$"        public const string {constName} = ""{constName}"";" + "\n\n";
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
        private static string GetDictHeader()
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


        private static string GetDictItems(IBlockchain blockchain, ref string constAsset)
        {
            StringBuilder assetDictItem = new();
            assetDictItem.Append(@$"
            assets.Add(
                AssetsConsts.{constAsset},
                new Coin
                {{
                    Name = @""{blockchain.Name.EscapeQuotes()}"",
                    Code = ""{blockchain.Code}"",");

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
