using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.ModelsFolder
{
    public class Token : Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }
      
    }
}
