using System;
using System.Collections.Generic;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.FolderModels
{

    /// <summary>
    /// Temporary test model to convert AssetSymbol to string before direct conversion
    /// due to limitation from Json Deserializer
    /// </summary>
    public class CoinFolder
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Research { get; set; }
        public Uri Explorer { get; set; }
        public AssetType Type { get; set; } // = AssetType.Coin;
        public Social[] Socials { get; set; }
        public string[] Tags { get; set; }
        public string Status { get; set; }
        public byte[] LogoPng { get; set; }
        public IList<Validator> Validators { get; set; }
    }
}
