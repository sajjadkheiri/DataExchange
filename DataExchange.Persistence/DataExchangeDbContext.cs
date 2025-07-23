using DataExchange.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DataExchange.Presentation;

public class DataExchangeDbContext(DbContextOptions<DataExchangeDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}