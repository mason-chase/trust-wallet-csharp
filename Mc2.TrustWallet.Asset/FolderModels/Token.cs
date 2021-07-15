using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    public class Token : Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
      
        public string Blockchain { get; set; }
    }
}
