namespace DataExchange.Domain.Model;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreatedAt { get; set; }
}
