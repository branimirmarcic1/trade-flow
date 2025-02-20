using Rate.API.Hubs;
using Rate.API.Kafka;
using Rate.API.Models;
using Rate.API.Rates.GetRates;
using System.Text.Json;

namespace Rate.API.Jobs;

public class RatesJob
{
    private readonly IHubContext<RateHub> _hubContext;
    private readonly IKafkaProducer _kafkaProducer;

    public RatesJob(IHubContext<RateHub> hubContext, IKafkaProducer kafkaProducer)
    {
        _hubContext = hubContext;
        _kafkaProducer = kafkaProducer;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        GetRatesResult dummyData = GenerateDummyRates();
        await _hubContext.Clients.All.SendAsync("ReceiveRates", dummyData, cancellationToken);

        string message = JsonSerializer.Serialize(dummyData);
        await _kafkaProducer.ProduceAsync("rate-updates", message);
    }

    private GetRatesResult GenerateDummyRates()
    {
        Random random = new Random();
        string[] symbols = new[] { "BTC", "ETH", "XRP", "DOGE", "ADA" };

        List<ExchangeRateDto> rates = symbols.Select(symbol => new ExchangeRateDto
        {
            Symbol = symbol,
            Price = Math.Round((decimal)random.NextDouble() * 50000, 2),
            LastUpdated = DateTimeOffset.UtcNow
        }).ToList();

        List<RateVariationAlert> variations =
        [
            new("BTC", 45000, 48000, 6.67m),
            new("ETH", 3000, 3200, 6.67m)
        ];

        return new GetRatesResult(rates, variations);
    }
}
