using System.IO;

namespace TrustWallet.Asset.Data
{
    public static class Settings
    {
        public static char DS => Path.DirectorySeparatorChar;
        public static string DataPath => $"..{DS}..{DS}..{DS}..{DS}TrustWallet.Asset{DS}Data";
        public static string LogoPath => $"{DataPath}{DS}Logos";
        public static string AssetSymbolsPath => $"{DataPath}{DS}AssetSymbols.cs";
        public static string AssetsDictPath => $"{DataPath}{DS}AssetsDict.cs";
    }
}
