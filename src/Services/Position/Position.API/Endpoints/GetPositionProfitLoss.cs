using Position.Application.Positions.Queries.GetPositionProfitLoss;

namespace Position.API.Endpoints;

public class GetPositionProfitLoss : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/positions/profit-loss", async (string instrumentId, ISender sender) =>
        {
            if (string.IsNullOrWhiteSpace(instrumentId))
            {
                return Results.BadRequest("InstrumentId is required.");
            }

            GetPositionProfitLossQuery query = new GetPositionProfitLossQuery(instrumentId);
            decimal profitLoss = await sender.Send(query);

            return Results.Ok(new { InstrumentId = instrumentId, ProfitLoss = profitLoss });
        })
        .WithName("GetPositionProfitLoss")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get Position Profit/Loss")
        .WithDescription("Returns the total profit or loss for a given instrument.");
    }
}