using Position.Application.Positions.Commands.UpdatePosition;

namespace Position.API.Endpoints;

public record UpdatePositionRequest(string InstrumentId, decimal NewRate);

public class UpdatePosition : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/positions/update-rate", async (UpdatePositionRequest request, ISender sender) =>
        {
            UpdatePositionCommand command = new UpdatePositionCommand(request.InstrumentId, request.NewRate);

            bool success = await sender.Send(command);

            if (!success)
            {
                return Results.NotFound($"No positions found for instrument {request.InstrumentId}");
            }

            return Results.Ok($"Updated positions for instrument {request.InstrumentId}");
        })
        .WithName("UpdatePosition")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Update Position Rate")
        .WithDescription("Updates all positions with the given instrument ID when the rate changes");
    }
}