using Mc2.TrustWallet.Asset.FolderModels.BlockchainProperties.TokenListProperties.BTokenProperties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mc2.TrustWallet.Asset.FolderModels.BlockchainProperties.TokenListProperties
{
    public class BToken
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("decimals")]
        public byte Decimals { get; set; }
        [JsonProperty("chainId")]
        public uint ChainId { get; set; }
        [JsonProperty("logoURI")]
        public Uri LogoUri { get; set; }
        [JsonProperty("asset")]
        public List<Pair> Pairs { get; set; }
    }
}
