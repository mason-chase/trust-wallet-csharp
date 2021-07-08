using System;
using TrustWallet.Asset.ModelsStandard.Interfaces;

namespace TrustWallet.Asset.ModelsStandard
{
    public class Link : ILink
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
    }

    public enum LinkType
    {
        Twitter,
        Telegram,
        Facebook,
        Discord,
        Github,
        WhitePaper
    }
}
