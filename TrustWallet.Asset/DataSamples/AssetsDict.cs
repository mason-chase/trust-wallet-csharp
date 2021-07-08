// <Header>
using System;
using System.IO;
using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.AssetProperties;
using TrustWallet.Asset.ModelsStandard.Interfaces;

namespace TrustWallet.Asset.Data.Samples
{
    public static partial class AssetsDict
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<string, IAsset> GetAssets()
        {
            Dictionary<string, IAsset> assets = new()
            {
// </Header>
// <Body>
                {
                    AssetsConsts.BTC_BITCOIN,
                    new Coin
                    {
                        Name = @"Aave Token",
                        Code = "AAVE",
                        Symbol = AssetSymbol.FromString(AssetsConsts.BTC_BITCOIN),
                        Website = new Uri("https://aave.com/"),
                        Description = @"Aave is a decentralized finance protocol that allows people to lend and borrow crypto.",
                        ShortDescription = "",
                        Explorer = new Uri("https://explorer.binance.org/asset/AAVE-8FA"),
                        Research = new Uri("https://research.binance.com/en/projects/aave-protocol"),
                        Decimals = 8,
                        Status = AssetStatus.Active,
                        LogoPng = File.ReadAllBytes(AssetsConsts.BTC_BITCOIN + ".png"),
                        Links = Array.Empty<Link>(),
                        Tags = Array.Empty<string>()
                    }
                }
// </Body>
// <Footer>
            };
            return assets;
        }
    }
}
// </Footer>