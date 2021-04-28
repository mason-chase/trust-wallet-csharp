using TrustWallet.Asset.Exceptions;
using TrustWallet.Asset.StandardModels;
using Xunit;

namespace TrustWallet.Asset.Tests
{
    public class AssetTests
    {
        [Theory]
        [InlineData(AssetSymbols.BTC)]
        [InlineData(AssetSymbols.ETH)]
        public void ParseAssetSymbolTest(string symbol)
        {
            AssetSymbol assetSymbol = AssetSymbol.FromString(symbol);
            Assert.Equal(symbol.ToUpper(), assetSymbol.Code);
        }

        [Fact]
        public void ParseAssetSymbolFailTest()
        {
            Assert.Throws<InvalidAssetSymbolException>(() => AssetSymbol.FromString("ZZZ"));
        }
    }
}
