using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TrustWallet.Asset.FolderModels;
using TrustWallet.Asset.StandardModels;
using ValidatorDto = TrustWallet.Asset.FolderModels.Validator;
using TokenStd = TrustWallet.Asset.StandardModels.Token;

namespace TrustWallet.Asset.Utilities
{
    public static class TwFolderTool
    {
        private static char DS { get; } = Path.DirectorySeparatorChar;
        private static string BuildPath { get; } = Directory.GetCurrentDirectory();
        
        #region Parse json settings
        /// <summary>
        /// Parse json settings
        /// </summary>
        private static readonly JsonSerializerSettings JsonSettingsSnakeCase = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()

            },
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Error
        };

        private static readonly JsonSerializerSettings JsonSettingsCamelCase = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()

            },
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Error
        };

        public static IDictionary<string, IAsset> GetAllAssets(IDictionary<string, BlockchainFolder> coins )
        {
            var coinsArray = coins.ToArray();
            IDictionary<string, IAsset> assets = new Dictionary<string, IAsset>();
            var coinMapper = Mappings.CoinMapperConfiguration.CreateMapper();
            var tokenMapper = Mappings.ValidatorMapperConfiguration.CreateMapper();
            foreach(var coin in coinsArray)
            {
                assets.Add(coin.Value.Coin.Symbol, coinMapper.Map<Coin>(coin.Value.Coin));
                foreach(TokenAsset asset in coin.Value.Coin.TokenAsset)
                {
                    var token = tokenMapper.Map<TokenStd>(asset);
                    assets.Add(asset.Symbol, token);
                }
            }
            return assets;
        }
        #endregion

        /// <summary>
        /// Tuple of Coins with Symbol and second is without symbol,
        /// simplifying <see cref="TrustWalletDataParse.TrustWalletJsonParseTest()">Parsing Blockchain folder</see>
        /// </summary>
        /// <returns></returns>
        public static (IDictionary<string, BlockchainFolder>, IDictionary<string, BlockchainFolder>) GetBlockChainFolder()
        {
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(BuildPath, 4) +
                                          $"{DS}assets{DS}blockchains";

            var folders = Directory.GetDirectories(assetBlockchainsRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();
            
            Dictionary<string, BlockchainFolder> blockChainFolders = new ();
            Dictionary<string, BlockchainFolder> blockChainFoldersPending = new ();
            
            foreach (string blockChainPath in folders)
            {
                var blockChainFiles = Directory.EnumerateFiles(blockChainPath, "*.json", SearchOption.AllDirectories).ToArray();
                var infoJson = File.ReadAllText($"{blockChainPath}{DS}info{DS}info.json");

                // Apparently .NET Json serializer is very lousy and is not able to understand snakeCase
                //Coin coin = JsonSerializer.Deserialize<Coin>(infoJson,  serializeOptions);
                CoinFolder coinString = JsonConvert.DeserializeObject<CoinFolder>(infoJson, JsonSettingsSnakeCase);
                if (coinString is null)
                    throw new NullReferenceException($"Check json file: {blockChainPath}{DS}info{DS}info.json \n" +
                                                     $"content was parsed as : {infoJson}");
                
                coinString.LogoPng = File.ReadAllBytes($"{blockChainPath}{DS}info{DS}logo.png");
                if (coinString.Symbol != null)
                {
                    if (Directory.Exists($"{blockChainPath}{DS}validators"))
                    {
                        coinString.Validators = GetValidators($"{blockChainPath}{DS}validators");
                    }

                    if (Directory.Exists($"{blockChainPath}{DS}assets"))
                    {
                        if (File.Exists($"{blockChainPath}{DS}assets{DS}info"))
                        {
                            var tokenFolder = GetTokenList($"{blockChainPath}");
                            coinString.Tokens = tokenFolder.Tokens;
                        }
                        coinString.TokenAsset = GetTokenAssetList($"{blockChainPath}");
                    }

                    blockChainFolders.Add(
                    coinString.Name.ToLower(),
                    new BlockchainFolder
                    {
                        Coin = coinString,
                        FolderName = "",
                        Files = blockChainFiles,
                        Info = infoJson
                    });
                }
                else
                {
                    blockChainFoldersPending.Add(
                        coinString.Name,
                        new BlockchainFolder
                        {
                            Coin = coinString,
                            FolderName = "",
                            Files = blockChainFiles,
                            Info = infoJson
                        });
                }
            }

            return (blockChainFolders,blockChainFoldersPending);
        }

        /// <summary>
        /// Parsing list.json within blockchain folder,
        /// simple static function to simplify shape of <see cref=GetBlockChainFolder/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<ValidatorDto> GetValidators(string path)
        {
            var validatorJson = File.ReadAllText($"{path}{DS}list.json");
            List<ValidatorDto> validators = JsonConvert.DeserializeObject<List<ValidatorDto>>(validatorJson, JsonSettingsCamelCase);

            foreach (ValidatorDto validator in validators)
            {
                validator.LogoPng = File.ReadAllBytes($"{path}{DS}assets{DS}{validator.Id}{DS}logo.png");
            }
            return validators;
        }

        public static TokenList GetTokenList(string path)
        {

            var tokenListJson = File.ReadAllText($"{path}{DS}tokenlist.json");
            TokenList tokenList = JsonConvert.DeserializeObject<TokenList>(tokenListJson, JsonSettingsCamelCase);

            return tokenList;
        }

        /// <summary>
        /// Read info.json and attach png and get address through folderpath.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<TokenAsset> GetTokenAssetList(string path)
        {

            var tokenAssetFiles = Directory.EnumerateFiles($"{path}{DS}assets", "info.json", SearchOption.AllDirectories).ToArray();
            List<TokenAsset> tokenAssetList = new();
            string infoJson;
            string basePath;
            foreach (string filePath in tokenAssetFiles)
            {
                infoJson = File.ReadAllText(filePath);
                TokenAsset tokenAsset = null; 
                try
                {
                    tokenAsset = JsonConvert.DeserializeObject<TokenAsset>(infoJson, JsonSettingsSnakeCase);
                }
                catch (JsonSerializationException ex)
                {
                    var message = ex.Message;
                }
                tokenAsset.Address = GetContractAddress(filePath);
                basePath = PathUtilities.RemoveFolder(filePath, 1);
                tokenAsset.LogoPng = File.ReadAllBytes($"{basePath}{DS}logo.png");
                tokenAssetList.Add(tokenAsset);
            }
            return tokenAssetList;
        }

        /// <summary>
        /// Get address part of folder path since not all info.json are included with address
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static string GetContractAddress(string filepath)
        {
            string resultString = Regex.Match(filepath, @"\\assets\\(?<address>[^\\.]*?)\\info\.json$").Groups["address"].Value;
            return resultString;
        }
    }
}
