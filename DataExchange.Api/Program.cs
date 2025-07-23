using DataExchange.Application.Order;
using DataExchange.Infra;
using DataExchange.Infra.Kafka;
using DataExchange.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataExchangeDbContext>(x =>
    x.UseSqlServer("Server=.;Database=DataExchangeDb;User Id=sa;Password=S@jj@d0910;TrustServerCertificate=True;"));

builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<IKafkaProducer, MockKafkaProducer>();
builder.Services.AddHostedService<OutboxPublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();