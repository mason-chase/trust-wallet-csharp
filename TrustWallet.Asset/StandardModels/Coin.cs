using System;
using System.Collections.Generic;
using TrustWallet.Asset.FolderModels;
using TrustWallet.Asset.StandardModels.CoinProperties;

namespace TrustWallet.Asset.StandardModels
{
    /// <summary>
    /// Coin is digital asset stored as native blockchain token
    /// </summary>
    public class Coin : IAsset
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AssetSymbol Symbol { get; set; }
        public string SymbolConst { get; set; }
        public byte Decimals { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Research { get; set; }
        public Uri Explorer { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public AssetType Type { get; set; } = AssetType.Coin;
        public Social[] Socials { get; set; } = Array.Empty<Social>();
        public string[] Tags { get; set; } = Array.Empty<string>();
        public AssetStatus Status { get; set; }
        /// <summary>
        /// List of Validators (Applies to POS coins only)
        /// </summary>
        public IList<Validator> Validators { get; set; }
        public byte[] LogoPng { get; set; }
        public IList<TokenAsset> TokenAsset { get; set; }
    }
}
