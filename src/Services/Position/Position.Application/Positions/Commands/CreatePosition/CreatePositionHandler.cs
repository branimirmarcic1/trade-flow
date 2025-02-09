using Position.Application.Data;
using PositionModel = Position.Domain.Models.Position;

namespace Position.Application.Positions.Commands.CreatePosition;

public class CreatePositionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreatePositionCommand, CreatePositionResult>
{
    public async Task<CreatePositionResult> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
    {
        PositionModel position = command.Position.Adapt<PositionModel>();

        dbContext.Positions.Add(position);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePositionResult(position.Id.Value);
    }
}