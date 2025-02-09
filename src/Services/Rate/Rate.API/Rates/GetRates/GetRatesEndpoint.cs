using BuildingBlocks.Exceptions;
using Rate.API.Models;
using Rate.API.Models.CoinMarketCap;

namespace Rate.API.Rates.GetRates;

public record GetRatesResponse(IEnumerable<ExchangeRate> ExchangeRates);

public class GetRatesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/rates", async (ISender sender, ICoinMarketCapApi coinMarketCapApi) =>
        {
            ApiResponse<Rootobject> apiResponse = await coinMarketCapApi.GetLatestListings();

            if (!apiResponse.IsSuccessStatusCode || apiResponse.Content == null)
            {
                throw new ApiResponseException("Failed to retrieve data from the CoinMarketCap API.");
            }

            List<ExchangeRate> exchangeRates = apiResponse.Content.Data.Adapt<List<ExchangeRate>>();

            GetRatesResponse response = new(exchangeRates);

            return Results.Ok(response);
        })
        .WithName("GetRates")
        .Produces<GetRatesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Rates")
        .WithDescription("Get Rates");
    }
}
