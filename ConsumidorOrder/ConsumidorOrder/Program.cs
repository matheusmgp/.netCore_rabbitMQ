using ConsumidorOrder.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace ConsumidorOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                while (true)
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var order = JsonSerializer.Deserialize<Order>(message);

                        Console.WriteLine($"Recebeu : Order Number {order.OrderNumber} |  {order.ItemName} | {order.Price}");

                    };
                    channel.BasicConsume(queue: "orderQueue",
                                         autoAck: true,
                                         consumer: consumer);

                }


            }
        }
    }
}
