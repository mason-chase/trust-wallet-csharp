using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TrustWallet.Asset.ModelsFolder;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.CoinProperties;
using TrustWallet.Asset.ModelsStandard.Interfaces;
using TrustWallet.Asset.Utilities.Exceptions;

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

        public static IDictionary<string, IAssetString> GetAllAssetsString(IDictionary<string, BlockchainFolder> coins)
        {
            var coinsArray = coins.ToArray();
            IDictionary<string, IAssetString> assetsDict = new Dictionary<string, IAssetString>();

            foreach (KeyValuePair<string, BlockchainFolder> assetKey in coinsArray)
            {
                assetKey.Value.Coin.Listings = new List<IListing> { new BlockchainListing(assetKey.Value.Coin.Code, assetKey.Value.Coin.Name) };
                assetsDict.Add(GetAssetsSymbol(assetKey.Value.Coin), assetKey.Value.Coin);
            }

            foreach (KeyValuePair<string, BlockchainFolder> coin in coinsArray)
            {
                foreach (TokenAsset token in coin.Value.Coin.TokenAsset)
                {
                    token.SymbolConst = GetAssetsSymbol(token);

                    if (assetsDict.ContainsKey(token.SymbolConst))
                    {
                        assetsDict.First(x => x.Key == token.SymbolConst)
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

        public static string GetAssetsSymbol(IAssetString asset)
        {
            string symbolConst = asset.Symbol.ToConstantCase();

            if (symbolConst.Length == 0)
                throw new BadSymbolNameException(asset);

            return $"{symbolConst}_{asset.Name.ToConstantCase()}";
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

            Dictionary<string, BlockchainFolder> blockChainFolders = new();
            Dictionary<string, BlockchainFolder> blockChainFoldersPending = new();

            foreach (string blockChainPath in folders)
            {
                string[] blockChainFiles = Directory.EnumerateFiles(blockChainPath, "*.json", SearchOption.AllDirectories).ToArray();
                string fileFullPath = $"{blockChainPath}{DS}info{DS}info.json";
                string infoJson = File.ReadAllText(fileFullPath);

                CoinFolder coinString;
                try
                {
                    coinString = JsonConvert.DeserializeObject<CoinFolder>(infoJson, JsonSettingsSnakeCase);

                }
                catch (JsonSerializationException ex)
                {
                    throw new BadJsonFileException(ex.Message, fileFullPath, infoJson);
                }

                if (coinString is null)
                    throw new NullReferenceException($"Check json file: {blockChainPath}{DS}info{DS}info.json \n" +
                                                     $"content was parsed as : {infoJson}");

                coinString.Code = GetBlockchainCode(blockChainPath);
                coinString.LogoPng = File.ReadAllBytes($"{blockChainPath}{DS}info{DS}logo.png");
                if (!string.IsNullOrEmpty(coinString.Symbol))
                {
                    if (Directory.Exists($"{blockChainPath}{DS}validators"))
                    {
                        coinString.Validators = GetValidators($"{blockChainPath}{DS}validators");
                    }

                    if (Directory.Exists($"{blockChainPath}{DS}assets"))
                    {
                        if (File.Exists($"{blockChainPath}{DS}assets{DS}info"))
                        {
                            TokenList tokenFolder = GetTokenList($"{blockChainPath}");
                            coinString.Tokens = tokenFolder.Tokens;
                        }
                        coinString.TokenAsset = GetTokenAssetList(blockChainPath);
                    }
                    else
                        coinString.TokenAsset = new List<TokenAsset>();

                    blockChainFolders.Add(
                    coinString.Name.ToLower(),
                    new BlockchainFolder
                    {
                        Coin = coinString,
                        FolderName = coinString.Code,
                        Files = blockChainFiles,
                        Info = infoJson
                    });
                }
                else
                {
                    blockChainFoldersPending.Add(
                        coinString.Code,
                        new BlockchainFolder
                        {
                            Coin = coinString,
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
        public static List<TokenAsset> GetTokenAssetList(string path)
        {

            var tokenAssetFiles = Directory.EnumerateFiles($"{path}{DS}assets", "info.json", SearchOption.AllDirectories).ToArray();
            List<TokenAsset> tokenAssetList = new();
            string infoJson;
            string basePath;
            foreach (string fileFullPath in tokenAssetFiles)
            {
                infoJson = File.ReadAllText(fileFullPath);
                TokenAsset tokenAsset = null;
                try
                {
                    tokenAsset = JsonConvert.DeserializeObject<TokenAsset>(infoJson, JsonSettingsSnakeCase);
                }
                catch (Exception ex) when (ex is JsonReaderException || ex is JsonSerializationException)
                {
                    throw new BadJsonFileException(ex.Message, fileFullPath, infoJson);
                }
                tokenAsset.Blockchain = GetBlockchainCode(fileFullPath);
                tokenAsset.Address = GetContractAddress(fileFullPath);
                basePath = PathUtilities.RemoveFolder(fileFullPath, 1);
                tokenAsset.LogoPng = File.ReadAllBytes($"{basePath}{DS}logo.png");
                if (string.IsNullOrEmpty(tokenAsset.Symbol))
                    throw new ArgumentNullException($"symbol is null: {tokenAsset.Symbol}");

                tokenAssetList.Add(tokenAsset);
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
