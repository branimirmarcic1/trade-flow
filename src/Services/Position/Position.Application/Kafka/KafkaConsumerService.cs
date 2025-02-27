using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Position.Application.Hubs;

namespace Position.Application.Kafka;

public class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly IHubContext<PositionHub> _hubContext;

    public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger, IHubContext<PositionHub> hubContext)
    {
        _logger = logger;
        ConsumerConfig config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:GroupId"] ?? "position-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _consumer.Subscribe("rate-updates");
        _hubContext = hubContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    ConsumeResult<Ignore, string> consumeResult = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"[Kafka] Received message: {consumeResult.Message.Value}");

                    // Ovdje možeš dodati logiku – npr. deserializirati poruku i kreirati novu poziciju

                    // Kreiraj poruku s trenutnim datumom i vremenom
                    string messageToSend = $"Ovo radi - {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

                    // Log poruke
                    _logger.LogInformation($"[Kafka] Sending message: {messageToSend}");

                    // Emitiraj poruku kroz SignalR
                    await _hubContext.Clients.All.SendAsync("ReceivePositionUpdate", messageToSend, stoppingToken);

                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"[Kafka] Error: {ex.Error.Reason}");
                }
            }
        }, stoppingToken);
    }

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}