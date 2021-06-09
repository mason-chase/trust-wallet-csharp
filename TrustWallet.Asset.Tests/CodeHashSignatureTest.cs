using System.Security.Cryptography;
using TrustWallet.Asset.Services;
using TrustWallet.Asset.Services.CodeSignatureServiceProperties;
using Xunit;

namespace TrustWallet.Asset.Tests
{
    public class CodeHashSignatureTest
    {
        /// <summary>
        /// Generated signature must match the embedded hash signature, otherwise the other must be used 
        /// to update the generated code before merge request.
        /// </summary>
        [Fact]
        public void GetSignaturesTest()
        {
            CodeSignatures signature = CodeSignatureService.GetSignatures();
            string assetConstsCs = signature.AssetConstsCs.ToString();
            string assetsDictCs = signature.AssetsDictCs.ToString();
            Assert.Equal(
                // Expected
                SHA256.Create().ToString(),
                // Calculated
                assetConstsCs
                );
        }

        [Fact]
        public void SetSignaturesTest()
        {
            CodeSignatures signature = CodeSignatureService.GetSignatures();
            string assetConstsCs = signature.AssetConstsCs.ToString();
            Assert.Equal(
                // Expected
                SHA256.Create().ToString(),
                // Calculated
                assetConstsCs
                );
        }
    }
}
