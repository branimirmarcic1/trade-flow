using Rate.API.Models;
using Rate.API.Models.CoinMarketCap;
using Rate.API.Rates.CreateRate;

namespace Rate.API.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<Datum, ExchangeRateDto>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Price, src => src.Quote.Usd.Price)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
        TypeAdapterConfig<Datum, ExchangeRate>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Price, src => src.Quote.Usd.Price)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
        TypeAdapterConfig<Datum, CreateRateCommand>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Price, src => src.Quote.Usd.Price)
            .Map(dest => dest.LastUpdated, src => src.LastUpdated);
    }
}