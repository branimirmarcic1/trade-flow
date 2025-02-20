namespace Rate.API.Kafka;

public class KafkaProducerService
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    public KafkaProducerService(IConfiguration configuration)
    {
        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
        _topic = configuration["Kafka:Topic"];
    }

    public async Task SendMessageAsync(string message)
    {
        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
    }
}