namespace TrustWallet.Asset.ModelsStandard
{
    public partial class Blockchain
    {
        private Blockchain(string code)
        {
            Code = code;
        }
        public string Code { get; }
        public string Name { get; }
        public byte Decimal { get; }
    }
}
