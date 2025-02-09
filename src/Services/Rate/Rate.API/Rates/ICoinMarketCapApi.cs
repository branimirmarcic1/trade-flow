using Rate.API.Models.CoinMarketCap;

namespace Rate.API.Rates;

public interface ICoinMarketCapApi
{
    [Get("/v1/cryptocurrency/listings/latest?convert=USD")]
    Task<ApiResponse<Rootobject>> GetLatestListings();
}