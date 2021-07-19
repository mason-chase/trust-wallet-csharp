using System.Collections.Generic;

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
