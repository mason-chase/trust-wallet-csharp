using AutoMapper;
using TrustWallet.Asset.ModelsFolder;
using TrustWallet.Asset.ModelsStandard;
using TrustWallet.Asset.ModelsStandard.AssetProperties;

namespace TrustWallet.Asset.Utilities
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
