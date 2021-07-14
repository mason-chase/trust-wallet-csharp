using System;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.ModelsStandard
{
    public class Link : ILink
    {
        public LinkType Name { get; set; }
        public Uri Url { get; set; }
    }

    public enum LinkType
    {
        Twitter,
        Telegram,
        Facebook,
        Discord,
        Github,
        WhitePaper,
        Reddit
    }
}
