using System.IO;

namespace Mc2.TrustWallet.Asset
{
    public static class Settings
    {
        public static char Ds => Path.DirectorySeparatorChar;
        public static string BuildPath { get; } = Directory.GetCurrentDirectory();
        public static string AssetsPathLocal => $"..{Ds}..{Ds}..{Ds}..{Ds}Mc2.TrustWallet.Asset{Ds}assets{Ds}blockchains";
        public static string DataPath => $"..{Ds}..{Ds}..{Ds}..{Ds}Mc2.TrustWallet.Asset{Ds}Data";
        public static string LogoPath => $"{DataPath}{Ds}Logos";
        public static string AssetsJsonPath => $"{DataPath}{Ds}AssetsJson";
        public static string ModelPath => $"..{Ds}..{Ds}..{Ds}..{Ds}Mc2.TrustWallet.Asset{Ds}ModelsStandard";
        public static string AssetConstsCsPath => $"{ModelPath}{Ds}AssetsConsts.cs";
        public static string AssetsDictCsPath => $"{ModelPath}{Ds}AssetsDict.cs";
        public static string LocalAssetsPath => $"{AssetsPathLocal}";
    }
}
