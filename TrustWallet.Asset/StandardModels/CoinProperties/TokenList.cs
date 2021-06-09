using System;
using System.Collections.Generic;
using TrustWallet.Asset.FolderModels;
using TokenVersion = TrustWallet.Asset.StandardModels.TokenListProperties.Version;
    
namespace TrustWallet.Asset.StandardModels.CoinProperties
{
    public class TokenList
    {
        public string Name { get; set; }
        public Uri LogoUri { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<Token> Tokens { get; set; }
        public TokenVersion Version { get; set; }
    }
}
