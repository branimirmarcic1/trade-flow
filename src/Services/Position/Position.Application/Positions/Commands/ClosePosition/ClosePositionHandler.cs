using Position.Application.Data;

namespace Position.Application.Positions.Commands.ClosePosition;

public class ClosePositionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ClosePositionCommand, int>
{
    public async Task<int> Handle(ClosePositionCommand command, CancellationToken cancellationToken)
    {
        List<Domain.Models.Position> positions = await dbContext.Positions
            .Where(p => p.InstrumentId == command.InstrumentId)
            .ToListAsync(cancellationToken);

        if (!positions.Any())
        {
            return 0;
        }

        dbContext.Positions.RemoveRange(positions);
        int deletedCount = await dbContext.SaveChangesAsync(cancellationToken);
        return deletedCount;
    }
}