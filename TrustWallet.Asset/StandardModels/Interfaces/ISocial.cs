﻿using System;

namespace TrustWallet.Asset.StandardModels
{
    public interface ISocial
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Handle { get; set; }
    }
}
