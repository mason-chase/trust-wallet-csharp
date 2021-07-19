using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels.BlockchainProperties.TokenListProperties.BTokenProperties
{
    public class Pair
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("lotSize")]
        public uint? LotSize { get; set; }
        [JsonProperty("tickSize")]
        public uint? TickSize { get; set; }
    }
}
