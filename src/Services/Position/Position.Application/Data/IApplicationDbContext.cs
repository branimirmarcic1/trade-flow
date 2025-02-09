using Microsoft.EntityFrameworkCore;

namespace Position.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Models.Position> Positions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
