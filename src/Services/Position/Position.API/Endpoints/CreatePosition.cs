using Position.Application.Positions.Commands.CreatePosition;

namespace Position.API.Endpoints;

public record CreatePositionRequest(
    string InstrumentId,
    decimal Quantity,
    decimal InitialRate,
    decimal CurrentRate,
    int Side
);

public record CreatePositionResponse(Guid Id);

public class CreatePosition : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/positions", async (CreatePositionRequest request, ISender sender) =>
        {
            CreatePositionCommand command = request.Adapt<CreatePositionCommand>();

            CreatePositionResult result = await sender.Send(command);

            CreatePositionResponse response = result.Adapt<CreatePositionResponse>();

            return Results.Created($"/positions/{response.Id}", response);
        })
        .WithName("CreatePosition")
        .Produces<CreatePositionResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Position")
        .WithDescription("Creates a new position");
    }
}