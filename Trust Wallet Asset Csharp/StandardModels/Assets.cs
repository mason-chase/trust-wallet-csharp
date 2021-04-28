using System.Collections.Generic;

namespace TrustWallet.Asset.StandardModels
{
    public static class Assets
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<AssetSymbol, IAsset> Dict { get; }

    }
}
