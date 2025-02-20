using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Position.Application.Kafka;

public class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger)
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
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
    {
        _consumer.Subscribe(_topic);
        while (!stoppingToken.IsCancellationRequested)
        {
                try
                {
                    ConsumeResult<Ignore, string> consumeResult = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"[Kafka] Received message: {consumeResult.Message.Value}");

                    // Ovdje možeš dodati logiku – npr. deserializirati poruku i kreirati novu poziciju
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