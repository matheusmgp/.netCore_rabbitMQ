using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using WebAppOrder.Entities;
using WebAppOrder.Interfaces;

namespace WebAppOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;
        private IMyConnectionFactory _myConnectionFactory;
       

        public OrderController(ILogger<OrderController> logger, IMyConnectionFactory myConnectionFactory)
        {
            _logger = logger;
            _myConnectionFactory = myConnectionFactory;
        }


        [HttpPost("insertOrder")]
        public IActionResult insertOrder(Order order)
        {
            try
            {
                var channel = _myConnectionFactory.GetChannel();
                string message = JsonSerializer.Serialize(order);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "Orders",
                                     routingKey: "orderQueueKey",
                                     basicProperties: null,
                                     body: body);
              
               
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
