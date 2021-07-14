using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Mc2.TrustWallet.Asset.Tests.Abstracts
{
    public abstract class TestBase
    {
        protected ITestOutputHelper OutputHelper { get; }

        public TestBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }
    }
}
