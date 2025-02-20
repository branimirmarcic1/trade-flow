using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Hangfire;
using Hangfire.MemoryStorage;
using Marten;
using Rate.API.Hubs;
using Rate.API.Jobs;
using Rate.API.Kafka;
using Rate.API.Mapping;
using Rate.API.Rates;

// Existing builder configuration...
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Register your services
MapsterSettings.Configure();
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// TODO: Move API Key and Base Address to configuration
builder.Services
    .AddRefitClient<ICoinMarketCapApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://pro-api.coinmarketcap.com");
        c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "549679b3-91d1-44aa-940a-9b0e5ed21e33");
    });
builder.Services.AddSingleton<KafkaProducerService>();

builder.Services.AddSignalR();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// ===================================

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

// === DODANO: CORS Middleware ===
app.UseCors("AllowAllOrigins");
// ================================

app.UseHangfireDashboard();

app.MapCarter();

app.MapHub<RateHub>("/ratehub");

RecurringJob.AddOrUpdate<RatesJob>(
    "RatesJob",
    job => job.ExecuteAsync(CancellationToken.None),
    Cron.Minutely);

app.UseExceptionHandler(options => { });
//app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
//{
//    ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
//});

app.Run();
