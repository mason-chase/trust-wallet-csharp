using System.Collections.Generic;

namespace TrustWallet.Asset.Data.Samples
{
    public partial class Blockchain
    {
        /// <summary>
        /// Collection of assets
        /// </summary>
        public static IDictionary<string, Blockchain> GetBlockchains()
        {
            Dictionary<string, Blockchain> blockchains = new()
            {
            #region Dictionary
                { BlockchainConsts.BITCOIN  , new Blockchain( BlockchainConsts.BITCOIN , "Bitcoin" , 10) },
                { BlockchainConsts.ETHEREUM , new Blockchain( BlockchainConsts.ETHEREUM, "Ethereum", 10) },
            #endregion
            };
            return blockchains;
        }
    }
}
