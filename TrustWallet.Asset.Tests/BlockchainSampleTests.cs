using TrustWallet.Asset.Data.Samples;
using TrustWallet.Asset.Tests.Abstracts;
using Xunit;
using Xunit.Abstractions;

namespace TrustWallet.Asset.Tests
{
    public class BlockchainSampleTests : TestBase
    {
        public BlockchainSampleTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Theory]
        [InlineData(BlockchainConsts.BITCOIN)]
        [InlineData(BlockchainConsts.ETHEREUM)]
        public void BlockchainValidationTest(string code)
        {
            Blockchain blockchain = Blockchain.FromString(code);
            Assert.Equal(code, blockchain.Code);
        }
    }
}
