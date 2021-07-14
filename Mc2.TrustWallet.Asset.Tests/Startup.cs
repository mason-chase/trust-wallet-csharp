using Microsoft.Extensions.DependencyInjection;

namespace Mc2.TrustWallet.Asset.Tests
{
    public class Startup
    {
#pragma warning disable CA1822 // Mark members as static
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            //services.AddTransient<ILoggerFactory>();
        }
    }
}
