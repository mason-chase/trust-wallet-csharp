using System;
using TrustWallet.Asset.ModelsStandard.Interfaces;

namespace TrustWallet.Asset.ModelsStandard
{
    public class Social : ISocial
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Handle { get; set; }
    }

    public enum SocialType
    {
        Twitter,
        Telegram,
        Facebook,
        Discord
    }
}
