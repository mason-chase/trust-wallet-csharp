using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Mc2.TrustWallet.Asset.FolderModels;
using Mc2.TrustWallet.Asset.FolderModels.CoinProperties;
using Mc2.TrustWallet.Asset.Utilities.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mc2.TrustWallet.Asset.Utilities
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

        public static IDictionary<string, Blockchain> GetBlockchains(IDictionary<string, BlockchainFolder> coins)
        {
            var coinsArray = coins.ToArray();
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(BuildPath, 4) +
                                          $"{DS}assets{DS}blockchains";

            var folders = Directory.GetDirectories(assetBlockchainsRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();


            IDictionary<string, IAsset> assetsDict = new Dictionary<string, IAsset>();

            foreach (KeyValuePair<string, BlockchainFolder> assetKey in coinsArray)
            {
                // Blockchain Coin no longer needs additional listing
                //assetKey.Value.Coin.TokenListings = new List<ITokenListing> { new BlockchainListing(assetKey.Value.Coin.Code, assetKey.Value.Coin.Name) };
                assetsDict.Add(GetAssetsSymbol(assetKey.Value.Coin), assetKey.Value.Coin);
            }

            foreach (KeyValuePair<string, BlockchainFolder> coin in coinsArray)
            {
                foreach (Token token in coin.Value.Coin.TokenAsset)
                {
                    token.Symbol = GetAssetsSymbol(token);

                    if (assetsDict.ContainsKey(token.SymbolConst))
                    {
                        assetsDict.First(x => x.Key == token.Symbol)
                                  .Value
                                  .TokenListings.Add( new TokenListing(token.Blockchain, token.Type, token.Id) );
                    }
                    else
                    {
                        token.TokenListings = new List<TokenListing>() { new TokenListing( token.Blockchain, token.Type, token.Id) };
                        assetsDict.Add(token.SymbolConst, token);
                    }
                }
            }
            return assetsDict;
        }

        public static string GetAssetsSymbol(Coin coin)
        {
            string symbolConst = coin.Name.ToConstantCase();

            if (symbolConst.Length == 0)
                throw new BadSymbolNameException(coin.Name);

            return $"{symbolConst}_{coin.Name.ToConstantCase()}";
        }
        #endregion

        /// <summary>
        /// Tuple of Coins with Symbol and second is without symbol,
        /// simplifying <see cref="TrustWalletDataParse.TrustWalletJsonParseTest()">Parsing Blockchain folder</see>
        /// </summary>
        /// <returns></returns>
        public static (IDictionary<string, BlockchainFolder>, 
            IDictionary<string, BlockchainFolder>) 
            GetBlockChainFolder()
        {
            string assetBlockchainsRoot = PathUtilities.RemoveFolder(BuildPath, 4) +
                                          $"{DS}assets{DS}blockchains";

            var folders = Directory.GetDirectories(assetBlockchainsRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();

            Dictionary<string, BlockchainFolder> blockChainFolders = new();

            foreach (string blockChainPath in folders)
            {
                string[] blockChainFiles = Directory.EnumerateFiles(blockChainPath, "*.json", SearchOption.AllDirectories).ToArray();
                string fileFullPath = $"{blockChainPath}{DS}info{DS}info.json";
                string infoJson = File.ReadAllText(fileFullPath);

                CoinFolder coinFolder;
                try
                {
                    coinFolder = JsonConvert.DeserializeObject<CoinFolder>(infoJson, JsonSettingsSnakeCase);

                }
                catch (JsonSerializationException ex)
                {
                    throw new BadJsonFileException(ex.Message, fileFullPath, infoJson);
                }

                if (coinFolder is null)
                    throw new NullReferenceException($"Check json file: {blockChainPath}{DS}info{DS}info.json \n" +
                                                     $"content was parsed as : {infoJson}");

                coinFolder = GetBlockchainCode(blockChainPath);
                coinFolder.LogoPng = File.ReadAllBytes($"{blockChainPath}{DS}info{DS}logo.png");
                if (!string.IsNullOrEmpty(coinFolder.Symbol))
                {
                    if (Directory.Exists($"{blockChainPath}{DS}validators"))
                    {
                        coinFolder.Validators = GetValidators($"{blockChainPath}{DS}validators");
                    }

                    if (Directory.Exists($"{blockChainPath}{DS}assets"))
                    {
                        if (File.Exists($"{blockChainPath}{DS}assets{DS}info"))
                        {
                            TokenList tokenFolder = GetTokenList($"{blockChainPath}");
                            coinFolder.Tokens = tokenFolder.Tokens;
                        }
                        coinFolder.TokenAsset = GetTokenAssetList(blockChainPath);
                    }
                    else
                        coinFolder.TokenAsset = new List<Token>();

                    blockChainFolders.Add(
                    coinFolder.Name.ToLower(),
                    new BlockchainFolder
                    {
                        Coin = coinFolder,
                        FolderName = coinFolder.Code,
                        Files = blockChainFiles,
                        Info = infoJson
                    });
                }
                else
                {
                    blockChainFoldersPending.Add(
                        coinFolder.Code,
                        new BlockchainFolder
                        {
                            Coin = coinFolder,
                            FolderName = blockChainPath,
                            Files = blockChainFiles,
                            Info = infoJson
                        });
                }
            }

            return (blockChainFolders, blockChainFoldersPending);
        }

        /// <summary>
        /// Parsing list.json within blockchain folder,
        /// simple static function to simplify shape of <see cref=GetBlockChainFolder/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Validator> GetValidators(string path)
        {
            var validatorJson = File.ReadAllText($"{path}{DS}list.json");
            List<Validator> validators = JsonConvert.DeserializeObject<List<Validator>>(validatorJson, JsonSettingsCamelCase);

            foreach (Validator validator in validators)
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
        public static List<Token> GetTokenAssetList(string path)
        {

            var tokenAssetFiles = Directory.EnumerateFiles($"{path}{DS}assets", "info.json", SearchOption.AllDirectories).ToArray();
            List<Token> tokenAssetList = new();
            string infoJson;
            string basePath;
            foreach (string fileFullPath in tokenAssetFiles)
            {
                infoJson = File.ReadAllText(fileFullPath);
                Token token = null;
                try
                {
                    token = JsonConvert.DeserializeObject<Token>(infoJson, JsonSettingsSnakeCase);
                }
                catch (Exception ex) when (ex is JsonReaderException || ex is JsonSerializationException)
                {
                    throw new BadJsonFileException(ex.Message, fileFullPath, infoJson);
                }
                token.Blockchain = GetBlockchainCode(fileFullPath);
                token.Address = GetContractAddress(fileFullPath);
                basePath = PathUtilities.RemoveFolder(fileFullPath, 1);
                token.LogoPng = File.ReadAllBytes($"{basePath}{DS}logo.png");
                if (string.IsNullOrEmpty(token.Symbol))
                    throw new ArgumentNullException($"symbol is null: {token.Symbol}");

                tokenAssetList.Add(token);
            }
            return tokenAssetList;
        }

        private static string GetBlockchainCode(string filepath)
        {
            string resultString = Regex.Match(filepath, @"[\\|/]assets[\\|/]blockchains[\\|/](?<folderName>\w{1,})").Groups["folderName"].Value;
            return resultString;
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
