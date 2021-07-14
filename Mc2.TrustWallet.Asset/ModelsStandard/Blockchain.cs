using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;
using System.Collections.Generic;

namespace Mc2.TrustWallet.Asset.ModelsStandard
{
    public partial class Blockchain : IBlockchain
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
