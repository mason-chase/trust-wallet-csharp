﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustWallet.Asset.Data.Samples
{
    public partial class Blockchain
    {
        public static Blockchain BITCOIN => new(BlockchainConsts.BITCOIN);
        public static Blockchain ETHEREUM => new(BlockchainConsts.ETHEREUM);
    }
}
