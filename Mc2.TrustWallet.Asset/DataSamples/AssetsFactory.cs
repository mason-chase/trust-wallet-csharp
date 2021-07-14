using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.DataSamples
{
    public class AssetsFactory
    {
        public static IAsset BtcBitcoin => Assets.FromSymbol(AssetSymbolFactory.BtcBitcoin);
        public static IAsset EthEthereum => Assets.FromSymbol(AssetSymbolFactory.EthEthereum);
        public static IAsset TrxTron => Assets.FromSymbol(AssetSymbolFactory.TrxTron);
        public static IAsset UsdtTether => Assets.FromSymbol(AssetSymbolFactory.UsdtTether);
    }
}