using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TrustWallet.Asset.FolderModels;
using ValidatorDto = TrustWallet.Asset.FolderModels.Validator;

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

            //var files = Directory.EnumerateFiles(assetRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();
            var folders = Directory.GetDirectories(assetBlockchainsRoot, "*.*", SearchOption.TopDirectoryOnly).ToArray();
            //.Where(x => x.EndsWith("base.json"));

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

                    blockChainFolders.Add(
                    coinString.Symbol,
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

        public static List<TokenFolder>
        //public static ICollection<string, Coin)> MapCoin()
        //{
        //    IMapper iMapper = Mappings.MapperConfiguration.CreateMapper();

        //    Coin coin = iMapper.Map<CoinString, Coin>(coinString);

        //}
    }
}
