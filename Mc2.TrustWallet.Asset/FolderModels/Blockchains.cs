using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    public class Blockchains
    {

        public static IDictionary<string, Blockchain> BlockchainsDict { get; set; } = GetBlockchains();

        private static IDictionary<string, Blockchain> GetBlockchains()
        {

            return new Dictionary<string, Blockchain>();
        }
    }
}
