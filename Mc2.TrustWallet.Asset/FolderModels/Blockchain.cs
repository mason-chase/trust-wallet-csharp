using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.TrustWallet.Asset.FolderModels
{
    public class Blockchain
    {
        public Coin Coin { get; set; }
        public string Code { get; set; }
        public IList<Token> Tokens { get; set; }

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
