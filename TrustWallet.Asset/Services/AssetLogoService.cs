using System.Collections.Generic;
using System.IO;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using TrustWallet.Asset.Services;
using static TrustWallet.Asset.Data.Settings;

namespace TrustWallet.Asset.Utilities
{
    public static class AssetLogoService
    {
        /// <summary>
        /// Persist images that are not stored before and update their hash signature
        /// </summary>
        public static void Persist(ref IDictionary<string, IAssetString> assets)
        {
            Dictionary<string, string> logoHashes = CodeSignatureService.GetLogoHashes();
            // Todo: Generate hash of dictionary
            bool modified = false;
            foreach (KeyValuePair<string, IAssetString> asset in assets)
            {
                string logoPngHash = asset.Value.LogoPng.GetSHA256();

                if (logoHashes.TryGetValue(asset.Value.SymbolConst, out string assetHash))
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
                    logoHashes.Add(asset.Value.SymbolConst, logoPngHash);
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
            string path = LogoPath + DS + name + ".png";
            File.WriteAllBytes(path, bytes);
        }
    }
}
