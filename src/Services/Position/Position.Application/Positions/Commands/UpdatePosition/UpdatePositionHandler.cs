using Microsoft.EntityFrameworkCore;
using Position.Application.Data;

namespace Position.Application.Positions.Commands.UpdatePosition;

public class UpdatePositionHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdatePositionCommand, bool>
{
    public async Task<bool> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
    {
        List<Domain.Models.Position> positions = await dbContext.Positions
            .Where(p => p.InstrumentId == command.InstrumentId)
            .ToListAsync(cancellationToken);

        if (!positions.Any())
        {
            return false;
        }

        foreach (Domain.Models.Position? position in positions)
        {
            position.UpdateCurrentRate(command.NewRate);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}