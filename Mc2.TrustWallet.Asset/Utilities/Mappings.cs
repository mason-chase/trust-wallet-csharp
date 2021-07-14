using AutoMapper;
using Mc2.TrustWallet.Asset.ModelsFolder;
using Mc2.TrustWallet.Asset.ModelsStandard;
using Mc2.TrustWallet.Asset.ModelsStandard.AssetProperties;

namespace Mc2.TrustWallet.Asset.Utilities
{
    public static class Mappings
    {
        public static MapperConfiguration CoinMapperConfiguration { get; } = new MapperConfiguration(cfg =>
                cfg.CreateMap<CoinFolder, Coin>()
                // Convert String to AssetSymbol
                .ForMember(
                    dst => dst.Symbol,
                    map => map.MapFrom(source => AssetSymbol.FromString(source.Symbol) )
                )
         );

    }
}
