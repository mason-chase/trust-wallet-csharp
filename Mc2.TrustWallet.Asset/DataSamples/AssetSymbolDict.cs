// <Header>
using System.Collections.Generic;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.DataSamples
{
    public partial class AssetSymbol
    {

        private static Dictionary<string, AssetSymbol> GetAssetSymbolDict()
        {
            Dictionary<string, AssetSymbol> assets = new()
            {
                #region Dictionary

// </Header>
// <Body>
                { AssetSymbolConsts.BTC_BITCOIN, new AssetSymbol(AssetSymbolConsts.BTC_BITCOIN)},
                { AssetSymbolConsts.ETH_ETHEREUM, new AssetSymbol(AssetSymbolConsts.ETH_ETHEREUM)},
                { AssetSymbolConsts.TRX_TRON, new AssetSymbol(AssetSymbolConsts.TRX_TRON)},
                { AssetSymbolConsts.USDT_TETHER, new AssetSymbol(AssetSymbolConsts.USDT_TETHER)}
// </Body>
// <Footer>
                #endregion
            };
            return assets;
        }
    }
}
// </Footer>