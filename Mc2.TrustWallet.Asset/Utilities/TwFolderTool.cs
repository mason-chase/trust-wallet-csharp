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
using static Mc2.TrustWallet.Asset.Settings;

namespace Mc2.TrustWallet.Asset.Utilities
{
    public static class TwFolderTool
    {
        #region Parse json settings
        /// <summary>
        /// Parse json settings
        /// </summary>
        private static readonly JsonSerializerSettings JsonSettingsSnakeCase = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()

            },
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Error
        };

        private static readonly JsonSerializerSettings JsonSettingsCamelCase = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()

            },
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Error
        };
        #endregion
        

        /// <summary>
        /// Generate a blockchain and parse Json files and attach Png assets to the object
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        public static Blockchain GetBlockchain(string blockchainPath)
        {
            return new Blockchain()
            {
                Code = GetBlockchainCode(blockchainPath),
                Coin = GetCoin(blockchainPath),
                Tokens = GetTokenAssetList(blockchainPath),
                Validators = GetValidators(blockchainPath),
                AllowList = GetAllowList(blockchainPath),
                DenyList = GetDenyList(blockchainPath),
                TokenList = GetTokenList(blockchainPath),
            };
        }

        /// <summary>
        /// Parse Coin from blockchain/info/asset.json and attach logo.png
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        private static Coin GetCoin(string blockchainPath)
        {
            string fileFullPath = $"{blockchainPath}{Ds}info{Ds}info.json";
            string infoJson = File.ReadAllText(fileFullPath);
            Coin coin;
            try
            {
                coin = JsonConvert.DeserializeObject<Coin>(infoJson, JsonSettingsSnakeCase);
            }
            catch (JsonSerializationException ex)
            {
                throw new BadJsonFileException(ex.Message, fileFullPath, infoJson);
            }

            coin.LogoPng = File.ReadAllBytes($"{blockchainPath}{Ds}info{Ds}logo.png");
            return coin;
        }

        /// <summary>
        /// Read info.json and attach png and get address through folderpath.
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        private static List<Token> GetTokenAssetList(string blockchainPath)
        {
            string assetsPath = $"{blockchainPath}{Ds}assets";
            if (!Directory.Exists(assetsPath))
                return null;

            string[] tokenAssetFiles = Directory.EnumerateFiles(assetsPath, "info.json", SearchOption.AllDirectories).ToArray();
            List<Token> tokenAssetList = new List<Token>();
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
                token.LogoPng = File.ReadAllBytes($"{basePath}{Ds}logo.png");
                if (string.IsNullOrEmpty(token.Symbol))
                    throw new ArgumentNullException($"symbol is null: {token.Symbol}");

                tokenAssetList.Add(token);
            }
            return tokenAssetList;
        }


        /// <summary>
        /// Get list of Validators if the folder exists.
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        private static List<Validator> GetValidators(string blockchainPath)
        {
            string validatorPath = $"{blockchainPath}{Ds}validators";
            if (!Directory.Exists(validatorPath))
                return null;

            var validatorJson = File.ReadAllText($"{validatorPath}{Ds}list.json");
            List<Validator> validators = JsonConvert.DeserializeObject<List<Validator>>(validatorJson, JsonSettingsCamelCase);

            foreach (Validator validator in validators)
            {
                validator.LogoPng = File.ReadAllBytes($"{validatorPath}{Ds}assets{Ds}{validator.Id}{Ds}logo.png");
            }
            return validators;
        }

        /// <summary>
        /// Parse TokenList object from tokenlist.json at blockchain's root folder (if exists).
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static TokenList GetTokenList(string path)
        {
            string tokenListPath = $"{path}{Ds}tokenlist.json";
            if (!Directory.Exists(tokenListPath))
                return null;

            var tokenListJson = File.ReadAllText(tokenListPath);
            TokenList tokenList = JsonConvert.DeserializeObject<TokenList>(tokenListJson, JsonSettingsCamelCase);

            return tokenList;
        }



        /// <summary>
        /// Parse `allowlist.json` if exists
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        private static IList<string> GetAllowList(string blockchainPath)
        {
            string allowlistPath = $"{blockchainPath}{Ds}allowlist.json";
            if (!File.Exists(allowlistPath))
                return null;

            string allowListJson = File.ReadAllText(allowlistPath);
            IList<string> denyList = JsonConvert.DeserializeObject<IList<string>>(allowListJson, JsonSettingsSnakeCase);
            return denyList;
        }

        /// <summary>
        /// Parse `denylist.json`
        /// </summary>
        /// <param name="blockchainPath"></param>
        /// <returns></returns>
        private static IList<string> GetDenyList(string blockchainPath)
        {
            string denylistPath = $"{blockchainPath}{Ds}denylist.json";
            if (!File.Exists(denylistPath))
                return null;

            string allowListJson = File.ReadAllText(denylistPath);
            IList<string> denyList = JsonConvert.DeserializeObject<IList<string>>(allowListJson, JsonSettingsSnakeCase);
            return denyList;
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
