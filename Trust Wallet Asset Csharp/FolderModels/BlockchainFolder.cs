namespace TrustWallet.Asset.FolderModels
{
    public class BlockchainFolder
    {
        public CoinFolder Coin { get; set; }
        public string FolderName { get; set; }
        public string[] Files { get; set; }
        public string Info { get; set; }
    }
}
