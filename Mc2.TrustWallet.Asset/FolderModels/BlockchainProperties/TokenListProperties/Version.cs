using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels.TokenListProperties
{
    public class Version
    {
        [JsonProperty("major")]
        public string Major { get; set; }
        [JsonProperty("minor")]
        public string Minor { get; set; }
        [JsonProperty("patch")]
        public string Patch { get; set; }
    }
}
