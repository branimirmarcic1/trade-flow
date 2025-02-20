namespace Rate.API.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, string message);
}