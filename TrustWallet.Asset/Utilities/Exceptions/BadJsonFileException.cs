using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TrustWallet.Asset.Utilities.Exceptions
{
    public class BadJsonFileException : Exception
    {
        public BadJsonFileException(string message, string filePath, string jsonContent) : base($"Error parsing {filePath} Json model: {message}")
        {
            FilePath = filePath;
            JsonContent = jsonContent;
        }

        public string FilePath { get; }
        public string JsonContent { get; }
    }
}
