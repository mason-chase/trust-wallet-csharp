using System;
using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard.TokenProperties;

namespace TrustWallet.Asset.ModelsStandard.Interfaces
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
        public Uri Explorer { get; set; }
        public Uri Research { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public string Type { get; set; }
        public Link[] Links { get; set; }
        public ActiveStatus Status { get; set; }
        public string[] Tags { get; set; }
        public byte[] LogoPng { get; set; }
        public IList<IListing> Listings { get; set; }
        public IList<TokenListing> TokenListings { get; set; }
        public IList<BlockchainListing> Blockchains { get; set; }

    }

    public interface IListing
    {
        ListingType Type { get; }
    }

    public enum ListingType
    {
        Token,
        Blockchain
    }

    public class BlockchainListing : IListing
    {
        public BlockchainListing(string code, string name)
        {
            Code = code;
            Name = name;
        }
        public ListingType Type { get; } = ListingType.Blockchain;
        public string Code { get; }
        public string Name { get; }

    }

    public class TokenListing : IListing
    {
        public TokenListing(string blockchain, string smartContractType, string id)
        {
            Blockchain = blockchain;
            SmartContractType = smartContractType;
            Id = id;
        }
        public ListingType Type { get; } = ListingType.Token;
        public string Blockchain { get; set; }
        public string SmartContractType { get; set; }
        public string Id { get; set; }
    }
}
