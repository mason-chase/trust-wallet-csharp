using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Mc2.TrustWallet.Asset.Utilities;
using Mc2.TrustWallet.Asset.FolderModels;
using static System.String;
using static Mc2.TrustWallet.Asset.Settings ;

namespace Mc2.TrustWallet.Asset.Services
{
    public class MigrationService
    {
        private readonly ILogger _logger;

        public MigrationService(ILogger<MigrationService> logger)
        {
            _logger = logger;
        }

        public void Rebuild()
        {
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(BuildPath, 4) +
                                          $"{Ds}assets{Ds}blockchains";
            
            DataRepository dataRepository = new DataRepository();

            IDictionary<string,Blockchain> blockchains = dataRepository.GetBlockchains();
            _logger.LogInformation(Format("Parsed {0} blockchain(s).", blockchains.Count));

            dataRepository.PersistBlockchains(blockchains);
        }
    }
}
