using System.Collections.Generic;
using TrustWallet.Asset.Exceptions;

namespace TrustWallet.Asset.StandardModels
{
    public class AssetSymbol
    {
        private AssetSymbol(string code)
        {
            Code = code;
        }

        public string Code { get; }

        public override string ToString()
        {
            return Code;
        }

        #region Dictionary
        private static Dictionary<string, AssetSymbol> Codes { get; } = new()
        {
            { AssetSymbols.AE, new AssetSymbol(AssetSymbols.AE) },
            { AssetSymbols.BTC, new AssetSymbol(AssetSymbols.BTC) },
            { AssetSymbols.ETH, new AssetSymbol(AssetSymbols.ETH) },
        };
        #endregion

        /// <summary>
        /// Create GdCountryCode from string value (case insensitive)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AssetSymbol FromString(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new InvalidAssetSymbolException();


            code = code.Trim().ToUpper();
            if (Codes.TryGetValue(code, out AssetSymbol gdCountryCode))
                return gdCountryCode;

            throw new InvalidAssetSymbolException();
        }

        #region Symbol Names
        /// <summary>
        /// Abkhazia (AB)
        /// </summary>
        public static AssetSymbol Bitcoin => new(AssetSymbols.BTC);

        #endregion
    }
}
