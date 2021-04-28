using System;
using System.Collections.Generic;
using System.Text;

namespace TrustWallet.Asset.Models
{
    public class Coin : IAsset
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public byte Decimal { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string SourceCode { get; set; }
        public string WhitePaper { get; set; }
        public string Explorer { get; set; }
        public AssetType Type { get; set; } = AssetType.Coin;
        public Social[] Socials { get; set; }
        public string[] Tags { get; set; }
    }
}
