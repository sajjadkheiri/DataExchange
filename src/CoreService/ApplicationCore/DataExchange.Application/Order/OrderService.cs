using System.Text.Json;
using DataExchange.Domain.Model;
using DataExchange.Presentation;

namespace DataExchange.Application.Order;

public class OrderService(DataExchangeDbContext dbContext)
{
    public async Task<Guid> CreateOrderAsync(string customerName)
    {
        var order = new Domain.Model.Order
        {
            Id = Guid.NewGuid(),
            CustomerName = customerName,
            CreatedAt = DateTime.UtcNow
        };

        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            AggregateType = "Order",
            AggregateId = order.Id,
            Type = "OrderCreated",
            Payload = JsonSerializer.Serialize(order),
            CreatedAt = DateTime.UtcNow
        };

        using var tx = await dbContext.Database.BeginTransactionAsync();

        dbContext.Orders.Add(order);
        dbContext.OutboxMessages.Add(outboxMessage);

        await dbContext.SaveChangesAsync();
        await tx.CommitAsync();

        return order.Id;
    }
}