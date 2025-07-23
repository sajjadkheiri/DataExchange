namespace DataExchange.Domain.Model;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string AggregateType { get; set; }
    public Guid AggregateId { get; set; }
    public string Type { get; set; } // e.g., "OrderCreated"
    public string Payload { get; set; } // Serialized JSON of the domain event
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}