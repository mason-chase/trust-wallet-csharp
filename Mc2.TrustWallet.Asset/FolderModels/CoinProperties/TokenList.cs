using System;
using System.Collections.Generic;
using Version = Mc2.TrustWallet.Asset.FolderModels.TokenListProperties.Version;

namespace Mc2.TrustWallet.Asset.FolderModels.CoinProperties
{
    public class TokenList
    {
        public string Name { get; set; }
        public Uri LogoUri { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<Token> Tokens { get; set; }
        public Version Version { get; set; }
    }
}
