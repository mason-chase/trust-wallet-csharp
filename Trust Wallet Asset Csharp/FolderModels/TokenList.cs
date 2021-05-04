using System;
using System.Collections.Generic;
using TokenVersion = TrustWallet.Asset.FolderModels.TokenListProperties.Version;
    
namespace TrustWallet.Asset.FolderModels
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
