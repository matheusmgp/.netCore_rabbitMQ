using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using WebAppOrder.Entities;

namespace WebAppOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }


        [HttpPost("insertOrder")]
        public IActionResult insertOrder(Order order)
        {
            try
            {

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                   
                    string message = JsonSerializer.Serialize(order);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "Orders",
                                         routingKey: "orderQueueKey",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
                return Accepted(order);
            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao tentar criar uma nova Order", e);

                return new StatusCodeResult(500);
            }
            
        }
    }
}
