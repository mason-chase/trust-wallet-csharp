using System;
using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using TrustWallet.Asset.ModelsStandard.TokenProperties;

namespace TrustWallet.Asset.ModelsStandard
{
    public class TokenAsset : IAssetString
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Symbol { get; set; }
        public string SymbolConst { get; set; }
        public byte Decimals { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public Uri Research { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public string Type { get; set; }
        public Social[] Socials { get; set; } = Array.Empty<Social>();
        public ActiveStatus Status { get; set; }
        public string[] Tags { get; set; } = Array.Empty<string>();
        public byte[] LogoPng { get; set; }
        public IList<TokenListing> TokenListings { get; set; } = new List<TokenListing>();
        public IList<BlockchainListing> Blockchains { get; set; } = new List<BlockchainListing>();
    }
}
