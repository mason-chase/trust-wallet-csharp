namespace Mc2.TrustWallet.Asset.ModelsStandard.Interfaces
{
    public interface ITokenListing
    {
        IBlockchain Blockchain { get; set; }
        byte Decimals { get; set; }
        string Id { get; set; }
        string TokenType { get; set; }
    }
}