using System;

namespace Mc2.TrustWallet.Asset.Services.CodeSignatureServiceProperties
{
    [Serializable]
    public class CodeSignatures
    {
        public string Assets { get; set; }
        public string AssetsDictCs { get; set; }
        public string AssetSymbolConstsCs { get; set; }
    }
}
