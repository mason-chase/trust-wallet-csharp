using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TrustWallet.Asset.Models;
using Xunit;
using static System.Environment;

namespace TrustWallet.Asset.Tests
{
    public class TrustWalletDataParse
    {

        [Fact]
        public void TrustWalletJsonParseTest()
        {
            var DS = Path.DirectorySeparatorChar;
            var buildPath = Directory.GetCurrentDirectory();
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(buildPath, 4) +
                                          $"{DS}assets{DS}blockchains";

            //var files = Directory.EnumerateFiles(assetRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();
            var files = Directory.GetDirectories(assetBlockchainsRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();
            //.Where(x => x.EndsWith("base.json"));

            List<BlockChainFolder> blockChainFolders = new List<BlockChainFolder>();

            foreach (string blockChainPath in files)
            {
                var blockChainFiles = Directory.EnumerateFiles(blockChainPath, "*.json", SearchOption.AllDirectories).ToArray();
                var infoJson = File.ReadAllText($"{blockChainPath}{DS}info{DS}info.json");
                Coin coin = JsonSerializer.Deserialize<Coin>(infoJson);
                blockChainFolders.Add(new BlockChainFolder
                {
                    Coin = coin,
                    FolderName = "",
                    Files = blockChainFiles
                });
            }
        }
    }

    public class BlockChainFolder
    {
        public Coin Coin { get; set; }
        public string FolderName { get; set; }
        public string[] Files { get; set; }
    }

    public static class PathUtilities
    {
        /// <summary>
        /// Remove N number of folder from right side of a path
        /// </summary>
        /// <param name="path">Pathname</param>
        /// <param name="folderCount">How many folder needed to be removed from right</param>
        /// <returns></returns>
        public static string RemoveFolder(string path, int folderCount)
        {
            var DS = Path.DirectorySeparatorChar;
            return String.Join(DS, path.Split(DS).Reverse().Skip(folderCount).Reverse());
        }

        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
