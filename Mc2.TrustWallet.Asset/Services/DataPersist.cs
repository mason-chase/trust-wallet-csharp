using Mc2.TrustWallet.Asset.FolderModels;
using Mc2.TrustWallet.Asset.FolderModels.CoinProperties;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Services
{
    public class DataPersist
    {
        private readonly ILogger<DataPersist> _logger;

        public DataPersist(ILogger<DataPersist> logger)
        {
            _logger = logger;
        }

        public static void PersistBlockchains(IDictionary<string, Blockchain> blockchains)
        {
            foreach (KeyValuePair<string, Blockchain> block in blockchains)
            {
                string blockchainPath = $"{LocalAssetsPath}{Ds}{block.Key}";
                Directory.CreateDirectory($"{blockchainPath}{Ds}info");
                string blockchainJson = JsonConvert.SerializeObject(block.Value.Coin, Formatting.Indented);
                File.WriteAllText($"{blockchainPath}{Ds}info{Ds}info.json", blockchainJson);
                File.WriteAllBytes($"{blockchainPath}{Ds}info{Ds}logo.png", block.Value.Coin.LogoPng);

                // Token Assets
                if (block.Value.Tokens is not null && block.Value.Tokens.Count > 0)
                {
                    #region Persist Token Assets
                    string tokenAssetsPath = $"{blockchainPath}{Ds}assets";
                    Directory.CreateDirectory(tokenAssetsPath);
                    foreach (Token token in block.Value.Tokens)
                    {
                        string tokenPath = $"{tokenAssetsPath}{Ds}{token.Id}";
                        Directory.CreateDirectory(tokenPath);
                        string tokenJson = JsonConvert.SerializeObject(token, Formatting.Indented);
                        File.WriteAllText($"{tokenPath}{Ds}info.json", tokenJson);
                        File.WriteAllBytes($"{tokenPath}{Ds}logo.png", token.LogoPng);
                    }
                    #endregion
                }

                // AllowList
                if (block.Value.AllowList is not null && block.Value.AllowList.Count > 0)
                {
                    string allowListJson = JsonConvert.SerializeObject(block.Value.AllowList, Formatting.Indented);
                    File.WriteAllText($"{blockchainPath}{Ds}allowlist.json", allowListJson);
                }

                // DenyList
                if (block.Value.DenyList is not null && block.Value.DenyList.Count > 0)
                {
                    string denyListJson = JsonConvert.SerializeObject(block.Value.AllowList, Formatting.Indented);
                    File.WriteAllText($"{blockchainPath}{Ds}denylist.json", denyListJson);
                }

                // Validators
                if (block.Value.Validators is not null && block.Value.Validators.Count > 0)
                {
                    string blockchainValidatorsPath = $"{blockchainPath}{Ds}validators";
                    Directory.CreateDirectory(blockchainValidatorsPath);
                    Directory.CreateDirectory($"{blockchainValidatorsPath}{Ds}assets");
                    string validatorJson = JsonConvert.SerializeObject(block.Value.Validators, Formatting.Indented);
                    File.WriteAllText($"{blockchainValidatorsPath}{Ds}list.json", validatorJson);

                    foreach (Validator validator in block.Value.Validators)
                    {
                        string validatorsAssetPath = $"{blockchainValidatorsPath}{Ds}assets{Ds}{validator.Id}";
                        Directory.CreateDirectory(validatorsAssetPath);
                        File.WriteAllBytes($"{validatorsAssetPath}{Ds}logo.png", validator.LogoPng);
                    }
                }
            }
        }
    }
}
