using System;
using TrustWallet.Asset.ModelsStandard.CoinProperties.ValidatorProperties;

namespace TrustWallet.Asset.ModelsStandard.CoinProperties
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
