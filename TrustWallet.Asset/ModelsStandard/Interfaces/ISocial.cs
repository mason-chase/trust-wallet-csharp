using System;

namespace TrustWallet.Asset.ModelsStandard.Interfaces
{
    public interface ISocial
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Handle { get; set; }
    }
}
