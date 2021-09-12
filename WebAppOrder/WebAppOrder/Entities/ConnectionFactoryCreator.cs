using RabbitMQ.Client;
using WebAppOrder.Interfaces;

namespace WebAppOrder.Entities
{
    public class ConnectionFactoryCreator : IMyConnectionFactory
    {
        public IModel _channel;
        public IConnection _connection;

        public ConnectionFactoryCreator()
        {
            CreateConnection();
        }
        public IModel GetChannel()
        {
            return _channel;
        }

        public void CreateConnection()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

      
    }
}
