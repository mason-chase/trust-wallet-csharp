using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustWallet.Asset.StandardModels;
using TrustWallet.Asset.StandardModels.TokenProperties;

namespace TrustWallet.Asset.FolderModels
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
        public IList<Pair> Pairs { get; set; }

        public string LogoUri { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
