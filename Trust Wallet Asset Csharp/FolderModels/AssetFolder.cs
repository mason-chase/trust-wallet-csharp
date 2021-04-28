using System;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.FolderModels
{
    public class Token
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public AssetType Type { get; set; }
        public Social[] Socials { get; set; }
        public AssetStatus Status { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
