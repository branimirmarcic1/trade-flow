using Rate.API.Models;
using Rate.API.Models.CoinMarketCap;

namespace Rate.API.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        // here we will define the type conversion / Custom-mapping
        // More details at https://github.com/MapsterMapper/Mapster/wiki/Custom-mapping

        // This one is actually not necessary as it's mapped by convention
        // TypeAdapterConfig<Product, ProductDto>.NewConfig().Map(dest => dest.BrandName, src => src.Brand.Name);.Split(";")[1]
        TypeAdapterConfig<Datum, ExchangeRate>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Price, src => (decimal)src.Quote.Usd.Price)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
    }
}