using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.ModelsStandard
{
    public partial class Blockchain
    {
        public static Blockchain BITCOIN => new(BlockchainConsts.BITCOIN);
        public static Blockchain ETHEREUM => new(BlockchainConsts.ETHEREUM);
    }
}
