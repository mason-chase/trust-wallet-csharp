
using Microsoft.CodeAnalysis.CSharp;
using RoslynCore;
using Xunit;
using Xunit.Abstractions;

namespace TrustWallet.Asset.Tests
{
    public class VideModelGenerationTest
    {
        // Pass ITestOutputHelper into the test class, which xunit provides per-test
        public VideModelGenerationTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        private ITestOutputHelper OutputHelper { get; }

        [Fact]
        public void ParseCodeTest()
        {
            const string models = @"namespace Models
{
  public class Item
  {
    public string ItemName { get; set }
  }
}
";
            var node = CSharpSyntaxTree.ParseText(models).GetRoot();
            var viewModel = ViewModelGeneration.GenerateViewModel(node);
            if (viewModel != null)
                OutputHelper.WriteLine(viewModel.ToFullString());
        }
    }
    
}
