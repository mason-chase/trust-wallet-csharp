using Mc2.TrustWallet.Asset.DataSamples;
using Mc2.TrustWallet.Asset.Exceptions;
using Xunit;

namespace Mc2.TrustWallet.Asset.Tests
{
    public class AssetSymbolTests
    {
        [Fact]
        public void ParseAssetSymbolFailTest()
        {
            Assert.Throws<InvalidAssetSymbolException>(() => AssetSymbol.FromString("ZZZ"));
        }
    }
}