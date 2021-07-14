using System;

namespace Mc2.TrustWallet.Asset.ModelsStandard.Interfaces
{
    public interface ILink
    {
        public LinkType Name { get; set; }
        public Uri Url { get; set; }
    }
}
