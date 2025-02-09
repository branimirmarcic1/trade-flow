using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Rate.API.Rates;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
System.Reflection.Assembly assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// TODO: Move API Key and Base Address to configuration
builder.Services
    .AddRefitClient<ICoinMarketCapApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://pro-api.coinmarketcap.com");
        c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "549679b3-91d1-44aa-940a-9b0e5ed21e33");
    });
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

//app.UseHealthChecks("/health",
//    new HealthCheckOptions
//    {
//        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//    });

app.Run();
