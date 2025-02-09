using BuildingBlocks.Exceptions;
using Rate.API.Models;
using Rate.API.Models.CoinMarketCap;
using Rate.API.Rates.CreateRate;

namespace Rate.API.Rates.GetRates;

public record GetRatesResponse(IEnumerable<ExchangeRateDto> ExchangeRates);

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

            List<CreateRateCommand> commands = apiResponse.Content.Data.Adapt<List<CreateRateCommand>>();

            CreateRatesResult result = await sender.Send(new CreateRatesCommand(commands));

            List<ExchangeRateDto> exchangeRates = commands.Adapt<List<ExchangeRateDto>>();
            return Results.Ok(new GetRatesResponse(exchangeRates));
        })
        .WithName("GetRates")
        .Produces<GetRatesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Rates")
        .WithDescription("Get Rates");
    }
}
