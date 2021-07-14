﻿using System.Collections.Generic;
using System.IO;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;
using Mc2.TrustWallet.Asset.Services;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Utilities
{
    public static class AssetLogoService
    {
        /// <summary>
        /// Persist images that are not stored before and update their hash signature
        /// </summary>
        public static void Persist(ref IDictionary<string, IAsset> assets)
        {
            Dictionary<string, string> logoHashes = CodeSignatureService.GetLogoHashes();
            // Todo: Generate hash of dictionary
            bool modified = false;
            foreach (KeyValuePair<string, IAsset> asset in assets)
            {
                string logoPngHash = asset.Value.LogoPng.GetSHA256();

                if (logoHashes.TryGetValue(asset.Value.Symbol.Code, out string assetHash))
                {
                    if (assetHash != logoPngHash)
                    {
                        WritePngFile(asset.Key, asset.Value.LogoPng);
                        logoHashes[asset.Key] = logoPngHash;
                        modified = true;
                    }
                }
                else
                {
                    logoHashes.Add(asset.Value.Symbol.Code, logoPngHash);
                    WritePngFile(asset.Key, asset.Value.LogoPng);
                    modified = true;
                }
            }

            if (modified)
            {
                CodeSignatureService.SaveLogoHashes(logoHashes);
            }
        }

        public static void WritePngFile(string name, byte[] bytes)
        {
            string path = LogoPath + Ds + name + ".png";
            File.WriteAllBytes(path, bytes);
        }
    }
}
