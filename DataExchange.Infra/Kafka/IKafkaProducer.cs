namespace DataExchange.Infra.Kafka;

public interface IKafkaProducer
{
    Task PublishAsync(string eventType, string payload);
}