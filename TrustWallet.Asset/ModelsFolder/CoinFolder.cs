using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using TrustWallet.Asset.ModelsStandard.TokenProperties;

namespace TrustWallet.Asset.ModelsFolder
{

    /// <summary>
    /// Temporary test model to convert AssetSymbol to string before direct conversion
    /// due to limitation from Json Deserializer
    /// </summary>
    public class CoinFolder : Coin , IAssetString
    {
        public new string Symbol { get; set; }
        public IList<Token> Tokens { get; set; }
        public IList<IListing> Listings { get; set; }
        public IList<TokenListing> TokenListings { get; set; } = new List<TokenListing>();
        public IList<BlockchainListing> Blockchains { get; set; } = new List<BlockchainListing>();
        string IAssetString.Type { get; set; } = "Blockchain";
        ActiveStatus IAssetString.Status { get; set; } = ActiveStatus.Active;

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Symbol: {Symbol}\n" +
                $"Decimals: {Decimals}\n" +
                $"Description: {Description}\n" +
                $"ShortDescription: {ShortDescription}\n" +
                $"Website: {Website}\n" +
                $"Research: {Research}\n";
        }
    }
}
