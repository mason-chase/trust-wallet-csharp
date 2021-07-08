using System;
using System.IO;
using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.AssetProperties;
using TrustWallet.Asset.ModelsStandard.Interfaces;

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
            return assets;
        }
    }
}
