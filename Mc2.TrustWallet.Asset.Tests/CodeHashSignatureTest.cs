using Mc2.TrustWallet.Asset.Services;
using Mc2.TrustWallet.Asset.Services.CodeSignatureServiceProperties;
using Xunit;

namespace Mc2.TrustWallet.Asset.Tests
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
            Assert.True(!string.IsNullOrEmpty(signature.AssetSymbolConstsCs));
            Assert.True(!string.IsNullOrEmpty(signature.AssetsDictCs));
        }

        [Fact]
        public void SaveSignaturesTest()
        {
            CodeSignatures signature = CodeSignatureService.GetSignatures();
            CodeSignatureService.SaveSignatures(new()
            {
                AssetSymbolConstsCs = signature.AssetSymbolConstsCs,
                AssetsDictCs = signature.AssetsDictCs
            }
            );
        }
    }
}