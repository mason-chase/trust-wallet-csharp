﻿using System;
using TrustWallet.Asset.ModelsStandard.AssetProperties;

namespace TrustWallet.Asset.ModelsStandard.Interfaces
{
    /// <summary>
    /// GlobalData's definition of a Cryptocurrency asset (Token, NativeToken, NFT, Smart Contracts)
    /// </summary>
    public interface IAsset
    {
        public string Name { get; set; }
        public AssetSymbol Symbol { get; set; }
        public string SymbolConst { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Uri Website { get; set; }
        public Uri Explorer { get; set; }
        public Uri Research { get; set; }
        public Uri AuditReport { get; set; }
        public Uri DataSource { get; set; }
        public AssetType Type { get; set; }
        public Link[] Links { get; set; }
        public AssetStatus Status { get; set; }
        public string[] Tags { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
