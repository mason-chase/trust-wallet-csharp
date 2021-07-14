using Mc2.TrustWallet.Asset.DataSamples;
using Mc2.TrustWallet.Asset.Tests.Abstracts;
using Xunit;
using Xunit.Abstractions;
using Blockchain = Mc2.TrustWallet.Asset.DataSamples.Blockchain;

namespace Mc2.TrustWallet.Asset.Tests
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
