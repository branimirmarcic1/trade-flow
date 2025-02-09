using Rate.API.Models;
using Rate.API.Models.CoinMarketCap;

namespace Rate.API.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<Datum, ExchangeRate>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Price, src => src.Quote.Usd.Price)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
    }
}