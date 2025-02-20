using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Position.Application.Kafka;

public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly string _topic;

    public KafkaConsumerService(IConfiguration configuration)
    {
        ConsumerConfig config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _topic = configuration["Kafka:Topic"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);
        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<Null, string> consumeResult = _consumer.Consume(stoppingToken);
            // Handle the consumed message
            await HandleMessageAsync(consumeResult.Message.Value);
        }
    }

    private Task HandleMessageAsync(string message)
    {
        // Implement your message handling logic here
        Console.WriteLine($"Consumed message: {message}");
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _consumer.Close();
        base.Dispose();
    }
}