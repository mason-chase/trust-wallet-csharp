namespace TrustWallet.Asset.ModelsFolder
{
    public class BlockchainFolder
    {
        public CoinFolder Coin { get; set; }
        public string FolderName { get; set; }
        public string[] Files { get; set; }
        public string Info { get; set; }
        public override string ToString()
        {
            return $"Coin: {Coin}\n" +
                $"FolderName: {FolderName}\n" +
                $"Files: {Files}\n" +
                $"Info: {Info}";
        }
    }
}
