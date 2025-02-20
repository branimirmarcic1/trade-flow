using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Position.Application.Data;
using Position.Application.Kafka;

namespace Position.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddHostedService<KafkaConsumerService>();
        return services;
    }
}
