using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustWallet.Asset.ModelsStandard;

namespace TrustWallet.Asset.Data.Samples
{
    public partial class Blockchain
    {
        private Blockchain(string code)
        {
            Code = code;
            Name = BlockchainDict[code].Name;
            Decimal = BlockchainDict[code].Decimal;
        }

        private Blockchain(string code, string name, byte vDecimal) 
        {
            Code = code;
            Name = name;
            Decimal = vDecimal;
        }

        public static Blockchain FromString(string code)
        {
            return BlockchainDict[code];
        }


        private static IDictionary<string, Blockchain> BlockchainDict => GetBlockchains();

        public string Code { get; }
        public string Name { get; }
        public byte Decimal { get; }
    }
}
