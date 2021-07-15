using System;
using Mc2.TrustWallet.Asset.FolderModels.CoinProperties.ValidatorProperties;

namespace Mc2.TrustWallet.Asset.FolderModels.CoinProperties
{
    public class Validator
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri Website { get; set; }
        public Payout Payout { get; set; }
        public Staking Staking { get; set; }
        public Status Status { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
