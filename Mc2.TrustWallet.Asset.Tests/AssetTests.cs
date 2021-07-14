using Mc2.TrustWallet.Asset.Exceptions;
using Mc2.TrustWallet.Asset.DataSamples;
using Mc2.TrustWallet.Asset.Tests.Abstracts;
using Xunit;
using Xunit.Abstractions;

namespace Mc2.TrustWallet.Asset.Tests
{
    public class AssetTests : TestBase
    {
        public AssetTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Fact]
        public void AssetsFactorySuccess()
        {
            Assert.True(string.IsNullOrEmpty(AssetsFactory.BtcBitcoin.Symbol.Code));
            Assert.True(string.IsNullOrEmpty(AssetsFactory.EthEthereum.Symbol.Code));
            Assert.True(string.IsNullOrEmpty(AssetsFactory.TrxTron.Symbol.Code));
            Assert.True(string.IsNullOrEmpty(AssetsFactory.UsdtTether.Symbol.Code));
        }

        [Fact]
        public void AssetSymbolFactorySuccess()
        {
            Assert.True(string.IsNullOrEmpty(AssetSymbolFactory.BtcBitcoin.Code));
            Assert.True(string.IsNullOrEmpty(AssetSymbolFactory.EthEthereum.Code));
            Assert.True(string.IsNullOrEmpty(AssetSymbolFactory.TrxTron.Code));
            Assert.True(string.IsNullOrEmpty(AssetSymbolFactory.UsdtTether.Code));
        }
    }
}
