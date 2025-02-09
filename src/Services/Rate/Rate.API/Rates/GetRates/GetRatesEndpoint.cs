using Carter;
using Mapster;
using MediatR;
using Rate.API.Models;

namespace Rate.API.Rates.GetRates;

public record GetRatesResponse(IEnumerable<ExchangeRate> ExchangeRates);

public class GetRatesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/rates", async (ISender sender) =>
        {
            GetRatesResult result = await sender.Send(new GetRatesQuery());

            GetRatesResponse response = result.Adapt<GetRatesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetRates")
        .Produces<GetRatesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Rates")
        .WithDescription("Get Rates");
    }
}
