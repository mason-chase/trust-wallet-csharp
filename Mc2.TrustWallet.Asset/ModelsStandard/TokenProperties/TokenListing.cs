using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.ModelsStandard.TokenProperties
{
    public class TokenListing : ITokenListing
    {
        public IBlockchain Blockchain { get; set; }
        public string Id { get; set; }
        public string TokenType { get; set; }
        public byte Decimals { get; set; }
    }
}