using System;
using System.Collections.Generic;
using TrustWallet.Asset.StandardModels;
using TrustWallet.Asset.StandardModels.TokenProperties;

namespace TrustWallet.Asset.FolderModels
{
    public interface IAssetString
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string SymbolConst { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public Uri Research { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public string Type { get; set; }
        public Social[] Socials { get; set; }
        public ActiveStatus Status { get; set; }
        public string[] Tags { get; set; }
        public byte[] LogoPng { get; set; }
        public IList<TokenListing> TokenListings { get; set; }
        public IList<BlockchainListing> Blockchains { get; set; }

    }

    public class BlockchainListing
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class TokenListing
    {
        public string Blockchain { get; set; }
        public string SmartContractType { get; set; }
        public string Id { get; set; }
    }
}
