using TrustWallet.Asset.Exceptions;
using TrustWallet.Asset.ModelsStandard.AssetProperties;
using Xunit;

namespace TrustWallet.Asset.Tests
{
    public class AssetTests
    {
        //[Theory]
        //[InlineData(AssetSymbols.BTC_BTC)]
        //[InlineData(AssetSymbols.ETH_ETHEREUM_TOKEN)]
        //public void ParseAssetSymbolTest(string symbol)
        //{
        //    AssetSymbol assetSymbol = AssetSymbol.FromString(symbol);
        //    Assert.Equal(symbol.ToUpper(), assetSymbol.Code);
        //}

        [Fact]
        public void ParseAssetSymbolFailTest()
        {
            Assert.Throws<InvalidAssetSymbolException>(() => AssetSymbol.FromString("ZZZ"));
        }
    }
}
