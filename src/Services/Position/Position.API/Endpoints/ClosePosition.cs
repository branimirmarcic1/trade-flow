using Position.Application.Positions.Commands.ClosePosition;

namespace Position.API.Endpoints;

public class ClosePosition : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/positions", async (string instrumentId, ISender sender) =>
        {
            ClosePositionCommand command = new ClosePositionCommand(instrumentId);
            int deletedCount = await sender.Send(command);

            if (deletedCount == 0)
            {
                return Results.NotFound($"No positions found for instrument {instrumentId}");
            }

            return Results.Ok($"Deleted {deletedCount} positions for instrument {instrumentId}");
        })
        .WithName("ClosePosition")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Close Position by InstrumentId")
        .WithDescription("Deletes all open positions for the given instrument.");
    }
}