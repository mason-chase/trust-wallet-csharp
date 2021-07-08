using Xunit;
using Xunit.Abstractions;
using TrustWallet.Asset.Services;

namespace TrustWallet.Asset.Tests
{
    public class TrustWalletDataParse
    {
        private ITestOutputHelper OutputHelper { get; }

        // Implementing further logging with LoggerFactory
        //private readonly ILogger Logger;
        //private ServiceProvider ServiceProvider { get; }

        public TrustWalletDataParse(ITestOutputHelper outputHelper/*, ILoggerFactory loggerFactory*/)
        {
            OutputHelper = outputHelper;

            //var services = new ServiceCollection()
            //                   .AddLogging(logging => logging.AddProvider(loggerProvider))
            //                   .BuildServiceProvider();
            //Logger = services.GetRequiredService<ILogger<TrustWalletDataParse>>();
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging();
            //ServiceProvider = serviceCollection.BuildServiceProvider();
        }

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

        [Fact]
        public void GenerateAssetSymbols()
        {
            //Assert.Equal(AssetSymbols.BTC_BITCOIN, Assets.Dict[AssetSymbols.BTC_BITCOIN].Symbol);
        }
    }
}
