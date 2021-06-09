using System;
using System.Collections.Generic;
using TrustWallet.Asset.StandardModels;
using TrustWallet.Asset.StandardModels.TokenProperties;

namespace TrustWallet.Asset.FolderModels
{

    /// <summary>
    /// Temporary test model to convert AssetSymbol to string before direct conversion
    /// due to limitation from Json Deserializer
    /// </summary>
    public class CoinFolder : Coin , IAssetString
    {
        public new string Symbol { get; set; }
        public IList<Token> Tokens { get; set; }
        public IList<TokenListing> TokenListings { get; set; }
        public IList<BlockchainListing> Blockchains { get; set; }
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
                $"SourceCode: {SourceCode}\n" +
                $"WhitePaper: {WhitePaper}\n" +
                $"Research: {Research}\n";
        }
    }
}
