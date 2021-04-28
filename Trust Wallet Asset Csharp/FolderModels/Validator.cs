namespace TrustWallet.Asset.FolderModels
{
    public class Validator : StandardModels.CoinProperties.Validator
    {
        /// <summary>
        /// Attach Image for reprocessing
        /// </summary>
        public byte[] LogoPng { get; set; }
    }
}
