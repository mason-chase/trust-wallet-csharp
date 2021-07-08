using System;

namespace TrustWallet.Asset.ModelsStandard.Interfaces
{
    public interface ILink
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
    }
}
