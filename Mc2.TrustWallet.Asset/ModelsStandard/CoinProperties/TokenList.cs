using System;
using System.Collections.Generic;
using TokenVersion = Mc2.TrustWallet.Asset.ModelsStandard.TokenListProperties.Version;
using Version = Mc2.TrustWallet.Asset.ModelsStandard.TokenListProperties.Version;

namespace Mc2.TrustWallet.Asset.ModelsStandard.CoinProperties
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
