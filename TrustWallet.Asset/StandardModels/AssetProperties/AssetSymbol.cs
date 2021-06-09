﻿using System.Collections.Generic;
using TrustWallet.Asset.Exceptions;
using TrustWallet.Asset.Data;

namespace TrustWallet.Asset.StandardModels
{
    public partial class AssetSymbol
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
        private static Dictionary<string, AssetSymbol> Codes { get; set; } = new();
        #endregion

        /// <summary>
        /// Create AssetSymbol from string value (case insensitive)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AssetSymbol FromString(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new InvalidAssetSymbolException();

            if (Codes.Count == 0)
            {
                //var assets = Assets.GetAssets();
                //foreach (var keyValue in assets)
                //{
                //    Codes.Add(
                //        keyValue.Key,
                //        new AssetSymbol(keyValue.Key)
                //        );
                //}
            }

            code = code.Trim().ToUpper();
            if (Codes.TryGetValue(code, out AssetSymbol assetSymbol))
                return assetSymbol;

            throw new InvalidAssetSymbolException();
        }
    }
}