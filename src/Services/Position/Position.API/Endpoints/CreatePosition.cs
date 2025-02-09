using Position.Application.Dtos;
using Position.Application.Positions.Commands.CreatePosition;

namespace Position.API.Endpoints;

public record CreatePositionRequest(PositionDto Order);
public record CreatePositionResponse(Guid Id);

public class CreatePosition : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/positions", async (CreatePositionRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreatePositionCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreatePositionResponse>();

            return Results.Created($"/positions/{response.Id}", response);
        })
        .WithName("CreatePosition")
        .Produces<CreatePositionResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Position")
        .WithDescription("Creates a new position");
    }
}