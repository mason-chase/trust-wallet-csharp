using System;
using System.IO;
using System.Linq;

namespace Mc2.TrustWallet.Asset.Utilities
{
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
            char DS = Path.DirectorySeparatorChar;
            return String.Join(DS.ToString(), path.Split(new char[] { DS }).Reverse().Skip(folderCount).Reverse());
        }
    }
}
