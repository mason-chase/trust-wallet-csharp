using System;
using System.Collections.Generic;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.FolderModels
{

    /// <summary>
    /// Temporary test model to convert AssetSymbol to string before direct conversion
    /// due to limitation from Json Deserializer
    /// </summary>
    public class CoinFolder : Coin
    {
        public new string Symbol { get; set; }
        public byte[] LogoPng { get; set; }
        public IList<Token> Tokens { get; set; }
        public IList<TokenAsset> TokenAsset { get; set; }
        public new IList<Validator> Validators { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}\n" +
                $"Symbol: {Symbol}\n" +
                $"Decimals: {Decimals}\n" +
                $"Description: {Description}\n" +
                $"ShortDescription: {ShortDescription}\n" +
                $"Website: {Website}\n" +
                $"SourceCode: {SourceCode}\n" +
                $"WhitePaper: {WhitePaper}\n" +
                $"Research: {Research}\n";
        }
    }
}
