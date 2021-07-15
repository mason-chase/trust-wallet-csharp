using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mc2.TrustWallet.Asset.Utilities;
using Microsoft.CodeAnalysis;
using static Mc2.TrustWallet.Asset.Settings;
using Azihub.Utilities.Base.Extensions.String;
using Mc2.TrustWallet.Asset.FolderModels;

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

            IDictionary<string,Blockchain> blockchains = TwFolderTool.GetBlockchains();
            _logger.LogInformation($"Parsed {blockchains.Count} blockchain(s).");

            DataPersist.PersistBlockchains(blockchains);
        }
    }
}
