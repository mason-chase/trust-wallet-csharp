using System.Collections.Generic;
using System.IO;
using Azihub.Utilities.Base.Extensions.ByteArray;
using Mc2.TrustWallet.Asset.FolderModels;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Services
{
    public static class AssetLogoService
    {
        /// <summary>
        /// Persist images that are not stored before and update their hash signature
        /// </summary>
        public static void Persist(ref IDictionary<string, AssetBase> assets)
        {
            Dictionary<string, string> logoHashes = CodeSignatureService.GetLogoHashes();
            // Todo: Generate hash of dictionary
            bool modified = false;
            foreach (KeyValuePair<string, AssetBase> asset in assets)
            {
                string logoPngHash = asset.Value.LogoPng.GetSha256();

                if (logoHashes.TryGetValue(asset.Value.Symbol, out string assetHash))
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
                    logoHashes.Add(asset.Value.Symbol, logoPngHash);
                    WritePngFile(asset.Key, asset.Value.LogoPng);
                    modified = true;
                }
            }

            if (modified)
            {
                CodeSignatureService.SaveLogoHashes(logoHashes);
            }
        }

        private static void WritePngFile(string name, byte[] bytes)
        {
            string path = LogoPath + Ds + name + ".png";
            File.WriteAllBytes(path, bytes);
        }
    }
}
