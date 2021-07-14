using Mc2.TrustWallet.Asset.Exceptions;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.DataSamples
{
    public partial class Assets
    {
        #region Dictionary
        private static Dictionary<string, IAsset> AssetsDict => GetAssets();
        #endregion

        /// <summary>
        /// Create AssetSymbol from string value (case insensitive)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IAsset FromSymbol(IAssetSymbol asset)
        {

            if (AssetsDict.Count == 0)
            {
                //var assets = Assets.GetAssets();
                //foreach (var keyValue in assets)
                //{
                //    Codes.Add(
                //        keyValue.Key,
                //        new AssetSymbol(keyValue.Key)
                //        );
                //}
            }

            if (AssetsDict.TryGetValue(asset.Code, out IAsset assetInstance))
                return assetInstance;

            throw new AssetWasNotFoundException(asset.Code);
        }
    }
}
