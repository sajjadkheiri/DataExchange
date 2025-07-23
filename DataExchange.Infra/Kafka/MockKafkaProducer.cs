using Microsoft.Extensions.Logging;

namespace DataExchange.Infra.Kafka;

public class MockKafkaProducer(ILogger<MockKafkaProducer> logger) : IKafkaProducer
{
    public Task PublishAsync(string eventType, string payload)
    {
        logger.LogInformation("[Kafka] Published event: {Type} with payload: {Payload}", eventType, payload);
        return Task.CompletedTask;
    }
}