using System.Collections.Generic;
using Xunit;
using TrustWallet.Asset.Utilities;
using TrustWallet.Asset.FolderModels;
using Xunit.Abstractions;
using System.Linq;
using System;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.Tests
{
    public class TrustWalletDataParse
    {
        private ITestOutputHelper OutputHelper { get; }
        public TrustWalletDataParse(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
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
            IDictionary<string, BlockchainFolder> blockchains;
            IDictionary<string, BlockchainFolder> blockchainsPending;
            (blockchains, blockchainsPending) = TwFolderTool.GetBlockChainFolder();

            //() = TwFolderTool.GetBlockChainFolder();

            IDictionary<string, IAsset> assets = TwFolderTool.GetAllAssets(blockchains);

            OutputHelper.WriteLine($"Parsed {blockchains.Count} blockchain");

            blockchains.Select(x => x.Value).ToList().ForEach(delegate (BlockchainFolder folder)
            {
                OutputHelper.WriteLine(folder.ToString());
            }) ;
            //OutputHelper.WriteLine(blockchains.Select(x => x.Value).ToArray().ToString());
        }

        [Fact]
        public void GenerateAssetSymbols()
        {
            
        }


    }


}
