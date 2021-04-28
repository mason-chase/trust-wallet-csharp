using Newtonsoft.Json;

namespace TrustWallet.Asset.FolderModels
{
    /// <summary>
    /// Parsing blockchains/{NAME}/validator/list.json along with logo
    /// </summary>
    [JsonArray]
    public class Validators
    {
        public Validator[] ValidatorArray { get; set; }
    }
}
