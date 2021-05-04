﻿using System;

namespace TrustWallet.Asset.StandardModels
{
    public interface IAsset
    {
        public string Name { get; set; }
        public AssetSymbol Symbol { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public Uri Research { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public AssetType Type { get; set; }
        public Social[] Socials { get; set; }
        public AssetStatus Status { get; set; }
        public string[] Tags { get; set; }
    }
}
