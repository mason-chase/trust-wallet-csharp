using System;
using Mc2.TrustWallet.Asset.FolderModels.CommonProperties;
using Newtonsoft.Json;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    public abstract class AssetBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description")]
        public string ShortDescription { get; set; }
        
        [JsonProperty("explorer")]
        public Uri Explorer { get; set; }

        [JsonProperty("research")]
        public Uri Research { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("decimals")]
        public byte Decimals { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("links")]
        public Link[] Links { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }
        
        public byte[] LogoPng { get; set; }
    }
}
