using System.Collections.Generic;
using Mc2.TrustWallet.Asset.ModelsStandard;
using Mc2.TrustWallet.Asset.ModelsStandard.AssetProperties;
using Mc2.TrustWallet.Asset.ModelsStandard.Interfaces;

namespace Mc2.TrustWallet.Asset.ModelsFolder
{

    /// <summary>
    /// Temporary test model to convert AssetSymbol to string before direct conversion
    /// due to limitation from Json Deserializer
    /// </summary>
    public class CoinFolder : Coin , IAsset
    {
        public new string Symbol { get; set; }
        public IList<Token> Tokens { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Symbol: {Symbol}\n" +
                $"Decimals: {Decimals}\n" +
                $"Description: {Description}\n" +
                $"ShortDescription: {ShortDescription}\n" +
                $"Website: {Website}\n" +
                $"Research: {Research}\n";
        }
    }
}
