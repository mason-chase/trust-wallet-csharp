using System;
using System.IO;
using System.Collections.Generic;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset
{
    public static partial class Assets
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<string, IAsset> GetAssets()
        {
            var assets = new Dictionary<string, IAsset>();
            #region DictData
            #endregion
            return assets;
        }
    }
}
