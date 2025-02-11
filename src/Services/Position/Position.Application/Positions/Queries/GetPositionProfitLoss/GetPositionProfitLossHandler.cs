using Position.Application.Data;

namespace Position.Application.Positions.Queries.GetPositionProfitLoss;

public record GetPositionProfitLossQuery(string InstrumentId) : IQuery<decimal>;

public class GetPositionProfitLossHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPositionProfitLossQuery, decimal>
{
    public async Task<decimal> Handle(GetPositionProfitLossQuery query, CancellationToken cancellationToken)
    {
        List<Domain.Models.Position> positions = await dbContext.Positions
            .Where(p => p.InstrumentId == query.InstrumentId)
            .ToListAsync(cancellationToken);

        if (!positions.Any())
        {
            return 0m;
        }

        decimal totalProfitLoss = positions.Sum(p => p.CalculateProfitLoss());
        return totalProfitLoss;
    }
}