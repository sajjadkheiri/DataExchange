using DataExchange.Application.Order;
using Microsoft.AspNetCore.Mvc;

namespace DataExchange.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataExchangeController(ILogger<DataExchangeController> logger, OrderService orderService) : ControllerBase
{
    [HttpGet(Name = "CreateOrder")]
    public async Task<IActionResult> CreateOrder()
    {
        logger.LogInformation("Start to Create Order");

        var result = await orderService.CreateOrderAsync("Sajjad");

        logger.LogInformation("Finish to Create Order");

        return Ok(result);
    }
}