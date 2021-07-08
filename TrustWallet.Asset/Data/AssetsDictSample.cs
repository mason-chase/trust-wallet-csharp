using System;
using System.IO;
using System.Collections.Generic;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.AssetProperties;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using AssetSymbols = TrustWallet.Asset.Data.AssetSymbolsSample;

namespace TrustWallet.Asset.Data
{
    public static partial class AssetsDictSample
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<string, IAsset> GetAssets()
        {
            Dictionary<string, IAsset> assets = new()
            {
                {
                    AssetSymbols.AAVE_AAVE_TOKEN,
                    new Coin
                    {
                        Name = @"Aave Token",
                        Code = "AAVE",
                        Symbol = AssetSymbol.FromString(AssetSymbols.AAVE_AAVE_TOKEN),
                        Website = new Uri("https://aave.com/"),
                        SourceCode = null,
                        WhitePaper = null,
                        Description = @"Aave is a decentralized finance protocol that allows people to lend and borrow crypto.",
                        ShortDescription = "",
                        Explorer = new Uri("https://explorer.binance.org/asset/AAVE-8FA"),
                        Research = new Uri("https://research.binance.com/en/projects/aave-protocol"),
                        Decimals = 8,
                        Status = AssetStatus.Active,
                        LogoPng = File.ReadAllBytes(AssetSymbols.AAVE_AAVE_TOKEN + ".png"),
                        Socials = Array.Empty<Social>(),
                        Tags = Array.Empty<string>()
                    }
                }
            };
            return assets;
        }
    }
}