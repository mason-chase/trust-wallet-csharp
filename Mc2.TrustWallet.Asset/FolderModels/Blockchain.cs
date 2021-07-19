using Mc2.TrustWallet.Asset.FolderModels.CoinProperties;
using System.Collections.Generic;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    public class Blockchain
    {
        /// <summary>
        /// Blockchain code: (folder name, example: "bitcoin")
        /// </summary>
        public string Code { get; set; }
        public Coin Coin { get; set; } = new Coin();
        public IList<Token> Tokens { get; set; }
        public IList<Validator> Validators { get; set; }
        public IList<string> AllowList { get; set; }
        public IList<string> DenyList { get; set; }
        public TokenList TokenList { get; set; }

        public override string ToString()
        {
            return $"Name: {Coin.Name}\n" +
                $"Symbol: {Code}\n" +
                $"Decimals: {Coin.Decimals}\n" +
                $"Description: {Coin.Description}\n" +
                $"ShortDescription: {Coin.ShortDescription}\n" +
                $"Website: {Coin.Website}\n" +
                $"Research: {Coin.Research}\n";
        }
    }
}
