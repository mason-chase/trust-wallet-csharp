using Mc2.TrustWallet.Asset.Tests.Abstracts;
using Xunit;
using Xunit.Abstractions;
using Mc2.TrustWallet.Asset.Services;
using Mc2.TrustWallet.Asset.Utilities;
using System.Collections.Generic;
using Mc2.TrustWallet.Asset.FolderModels;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Tests
{
    public class TrustWalletDataParse : TestBase
    {
        // Implementing further logging with LoggerFactory
        //private readonly ILogger Logger;
        //private ServiceProvider ServiceProvider { get; }

        public TrustWalletDataParse(ITestOutputHelper outputHelper) : base(outputHelper) { }


        
        [Fact]
        public void ReadTrustWalletOriginalAssetData()
        {
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(BuildPath, 4) +
                                          $"{Ds}assets{Ds}blockchains";

            DataRepository dataRepository = new(assetBlockchainsRoot);
            IDictionary<string, Blockchain> blockchains = dataRepository.GetBlockchains();
            
            Assert.True(blockchains.Count > 0);
        }


        [Fact]
        public void ReadTrustWalletGeneratedAssetData()
        {
            
            DataRepository dataRepository = new();
            IDictionary<string, Blockchain> blockchains = dataRepository.GetBlockchains();
            OutputHelper.WriteLine($"Parsed {blockchains.Count} blockchains");
            Assert.True(blockchains.Count > 0);
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
    }
}
