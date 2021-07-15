namespace Mc2.TrustWallet.Asset.FolderModels
{
    public class BlockchainFolder
    {
        public string FolderName { get; set; }
        public string[] Files { get; set; }
        public string Info { get; set; }
        public override string ToString()
        {
            return $"FolderName: {FolderName}\n" +
                $"Files: {Files}\n" +
                $"Info: {Info}";
        }
    }
}
