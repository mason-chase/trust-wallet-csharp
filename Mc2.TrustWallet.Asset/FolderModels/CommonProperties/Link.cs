using System;
using Mc2.TrustWallet.Asset.FolderModels.CommonProperties.LinkProperties;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mc2.TrustWallet.Asset.FolderModels.CommonProperties
{
    public class Link
    {
        [JsonProperty("name")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LinkTypes Type { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
