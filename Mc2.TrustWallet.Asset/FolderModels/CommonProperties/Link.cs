using System;
using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels.CommonProperties
{
    public class Link
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
