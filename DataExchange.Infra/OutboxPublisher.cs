using DataExchange.Infra.Kafka;
using DataExchange.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace DataExchange.Infra;

public class OutboxPublisher(IServiceProvider serviceProvider, ILogger<OutboxPublisher> logger, IKafkaProducer producer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataExchangeDbContext>();

            var unsentMessages = await dbContext.OutboxMessages
                .Where(m => m.ProcessedAt == null)
                .OrderBy(m => m.CreatedAt)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var message in unsentMessages)
            {
                try
                {
                    await producer.PublishAsync(message.Type, message.Payload);
                    message.ProcessedAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to publish message {Id}", message.Id);
                }
            }

            await dbContext.SaveChangesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}