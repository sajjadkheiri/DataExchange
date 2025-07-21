namespace DataExchange.Persistence;

public class DataExchangeDbContext : DbContext
{
    public DataExchangeDbContext(DbContextOptions<DataExchangeDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {            
            optionsBuilder.UseSqlServer("Server=localhost;Database=DataExchangeDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
