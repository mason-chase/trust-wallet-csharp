using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard.TokenProperties;

namespace TrustWallet.Asset.ModelsStandard
{
    public class Token
    {
        public string Name { get; set; }
        public string Asset { get; set; }
        public string Address { get; set; }
        public byte Decimals { get; set; }
        public string Status { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public IList<Pair> Pairs { get; set; } = new List<Pair>();

        public string LogoUri { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
