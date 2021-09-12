using RabbitMQ.Client;
using WebAppOrder.Interfaces;

namespace WebAppOrder.Entities
{
    public class RabbitMQUserClass
    {
        public ConnectionFactory ConnectionFactory { get; private set; }
        public RabbitMQUserClass(IMyConnectionFactory myCconnectionFactory)
        {
            //ConnectionFactory = myCconnectionFactory.Get("localhost");
        }
    }
}
