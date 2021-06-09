using TrustWallet.Asset.Utilities;
using Xunit;

namespace TrustWallet.Asset.Tests
{
    public class StringUtilityTests
    {
        [Theory]
        [InlineData("BITCOIN_ETHEREUM_COINS","Bitcoin Ethereum Coins")]
        [InlineData("_1BITCOIN_ETHEREUM_COINS","1Bitcoin Ethereum Coins")]
        public void ToConstantCase(string expected, string original)
        {
            string calculated = original.ToConstantCase();
            Assert.Equal(expected, calculated);
        }
    }
}
