using Mc2.TrustWallet.Asset.FolderModels.BlockchainProperties.TokenListProperties;
using System;
using System.Collections.Generic;
using Version = Mc2.TrustWallet.Asset.FolderModels.TokenListProperties.Version;

namespace TrustWallet.Asset.ModelsStandard.CoinProperties
{
    public class TokenList
    {
        public string Name { get; set; }
        public Uri LogoUri { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<BToken> Tokens { get; set; }
        public Version Version { get; set; }
    }
}
