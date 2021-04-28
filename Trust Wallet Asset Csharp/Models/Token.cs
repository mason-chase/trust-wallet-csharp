namespace TrustWallet.Asset.Models
{
    public class Token : IAsset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public byte Decimal { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string SourceCode { get; set; }
        public string WhitePaper { get; set; }
        public string Explorer { get; set; }
        public AssetType Type { get; set; }
        public Social[] Socials { get; set; }
    }
}
