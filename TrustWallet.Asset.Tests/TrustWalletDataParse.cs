using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using TrustWallet.Asset.Utilities;
using AutoMapper;
using Xunit.Abstractions;

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
            var (blockchains, blockchainsPending)= TwFolderTool.GetBlockChainFolder();
            OutputHelper.WriteLine(blockchainsPending.ToString());
            OutputHelper.WriteLine(blockchains.ToString());
        }

        [Fact]
        public void GenerateAssetSymbols()
        {
            
        }


    }


}
