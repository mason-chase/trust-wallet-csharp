using Xunit;
using Xunit.Abstractions;
using TrustWallet.Asset.Services;
using TrustWallet.Asset.Tests.Abstracts;

namespace TrustWallet.Asset.Tests
{
    public class TrustWalletDataParse : TestBase
    {
        // Implementing further logging with LoggerFactory
        //private readonly ILogger Logger;
        //private ServiceProvider ServiceProvider { get; }

        public TrustWalletDataParse(ITestOutputHelper outputHelper) : base(outputHelper) { }

        /// <summary>
        /// A) Parse blockchain folders
        /// B) Parse Validators
        /// C) Parse smart contracts (Tokens)
        /// D) Parse allow/deny lists
        /// </summary>
        [Fact]
        public void TrustWalletJsonParseTest()
        {
            //MigrationService migrationService = ServiceProvider.GetService<MigrationService>();
            MigrationService migrationService = new(OutputHelper.BuildLoggerFor<MigrationService>());
            migrationService.Rebuild();
        }
    }
}
