using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustWallet.Asset.StandardModels;

namespace TrustWallet.Asset.FolderModels
{
    public class TokenAsset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Symbol { get; set; }
        public byte Decimals { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Research { get; set; }
        public string AuditReport { get; set; }
        public Uri Website { get; set; }
        public Uri SourceCode { get; set; }
        public Uri WhitePaper { get; set; }
        public Uri Explorer { get; set; }
        public string DataSource{ get; set; }
        public string Type { get; set; }
        public Social[] Socials { get; set; }
        public string Status { get; set; }
        public string[] Tags { get; set; }
        public byte[] LogoPng { get; set; }
    }
}
