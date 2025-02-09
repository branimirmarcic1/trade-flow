using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Position.Infrastructure.Data.Extensions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedProductAsync(context);
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Positions.AnyAsync())
        {
            await context.Positions.AddRangeAsync(InitialData.Positions);
            await context.SaveChangesAsync();
        }
    }
}
