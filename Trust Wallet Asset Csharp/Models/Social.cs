﻿using System;

namespace TrustWallet.Asset.Models
{
    public class Social : ISocial
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Handle { get; set; }
    }
}
