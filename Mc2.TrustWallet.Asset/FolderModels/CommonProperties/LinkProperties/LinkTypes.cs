using System.Runtime.Serialization;

namespace Mc2.TrustWallet.Asset.FolderModels.CommonProperties.LinkProperties
{
    public enum LinkTypes
    {
        [EnumMember(Value = "blog")]
        Blog,
        [EnumMember(Value = "docs")]
        Docs,
        [EnumMember(Value = "coingecko")]
        Coingecko,
        [EnumMember(Value = "coinmarketcap")]
        CoinMarketCap,
        [EnumMember(Value = "discord")] 
        Discord,
        [EnumMember(Value = "facebook")]
        Facebook,
        [EnumMember(Value = "gitbook")]
        Gitbook,
        [EnumMember(Value = "github")]
        Github,
        [EnumMember(Value = "instagram")]
        Instagram,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "LinkedIn")]
        LinkedIn,
        [EnumMember(Value = "reddit")]
        Reddit,
        [EnumMember(Value = "source_code")]
        SourceCode,
        [EnumMember(Value = "telegram")]
        Telegram,
        [EnumMember(Value = "twitter")]
        Twitter,
        [EnumMember(Value = "telegram_news")]
        TelegramNews,
        [EnumMember(Value = "white_paper")]
        WhitePaper,
        [EnumMember(Value = "youtube")]
        Youtube,
    }
}
