using Mc2.TrustWallet.Asset.FolderModels;
using Mc2.TrustWallet.Asset.FolderModels.CoinProperties;
using Mc2.TrustWallet.Asset.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Services
{
    public class DataRepository
    {

        public DataRepository(string assetPath = null)
        {
            AssetPath = (assetPath is null ? LocalAssetsPath : assetPath);
        }

        public string AssetPath { get; }

        public IDictionary<string, Blockchain> GetBlockchains()
        {
            string[] blockchainsPath = Directory.GetDirectories(AssetPath, "*.*", SearchOption.TopDirectoryOnly).ToArray();

            IDictionary<string, Blockchain> blockchainsDict = new Dictionary<string, Blockchain>();

            foreach (string blockchainPath in blockchainsPath)
            {
                Blockchain blockchain = TwFolderTool.GetBlockchain(blockchainPath);
                blockchainsDict.Add(blockchain.Code, blockchain);
            }

            return blockchainsDict;
        }

        public void PersistBlockchains(IDictionary<string, Blockchain> blockchains)
        {
            // Bson
            //BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite($"{AssetPath}{Ds}Blockchains.bson"));
            //using (BsonDataWriter writer = new BsonDataWriter(binaryWriter))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(writer, blockchains);
            //}

            // Json
            foreach (KeyValuePair<string, Blockchain> block in blockchains)
            {
                string blockchainPath = $"{AssetPath}{Ds}{block.Key}";
                Directory.CreateDirectory($"{blockchainPath}{Ds}info");
                string blockchainJson = JsonConvert.SerializeObject(block.Value.Coin, Formatting.Indented);
                File.WriteAllText($"{blockchainPath}{Ds}info{Ds}info.json", blockchainJson);
                File.WriteAllBytes($"{blockchainPath}{Ds}info{Ds}logo.png", block.Value.Coin.LogoPng);

                // Token Assets
                if (block.Value.Tokens != null && block.Value.Tokens.Count > 0)
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
                if (block.Value.AllowList != null && block.Value.AllowList.Count > 0)
                {
                    string allowListJson = JsonConvert.SerializeObject(block.Value.AllowList, Formatting.Indented);
                    File.WriteAllText($"{blockchainPath}{Ds}allowlist.json", allowListJson);
                }

                // DenyList
                if (block.Value.DenyList != null && block.Value.DenyList.Count > 0)
                {
                    string denyListJson = JsonConvert.SerializeObject(block.Value.AllowList, Formatting.Indented);
                    File.WriteAllText($"{blockchainPath}{Ds}denylist.json", denyListJson);
                }

                // Validators
                if (block.Value.Validators != null && block.Value.Validators.Count > 0)
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
