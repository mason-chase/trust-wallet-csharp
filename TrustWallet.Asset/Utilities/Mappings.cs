using AutoMapper;
using TrustWallet.Asset.FolderModels;
using TrustWallet.Asset.StandardModels;

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
