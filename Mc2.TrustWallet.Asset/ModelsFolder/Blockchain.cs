using Mc2.TrustWallet.Asset.ModelsFolder.CommonProperties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.ModelsFolder
{
    public class Blockchain
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("explorer")]
        public Uri Explorer { get; set; }

        [JsonProperty("research")]
        public string Research { get; set; }

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

        public byte[] Logo { get; set; }
    }
}
