using System;

namespace TrustWallet.Asset.StandardModels
{
    /// <summary>
    /// Token is digital asset running on top of another Blockchain and Smart Contract type,
    /// Except Cardano Native Token that does not need Smart Contract most Blockchain offer sort of Token
    /// that is issued through Smart Contract programming.
    /// </summary>
    public class Token : IAsset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AssetSymbol Symbol { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public AssetType Type { get; set; }
        public Social[] Socials { get; set; }
        public AssetStatus Status { get; set; }
    }
}
