namespace Mc2.TrustWallet.Asset.ModelsStandard.Interfaces
{
    public interface IBlockchain
    {
        string Code { get; }
        byte Decimal { get; }
        string Name { get; }
    }
}