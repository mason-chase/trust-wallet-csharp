using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    /// <summary>
    /// Blockchain.AssetToken
    /// </summary>
    public class Token : Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("blockchain")]
        public string Blockchain { get; set; }
    }
}
