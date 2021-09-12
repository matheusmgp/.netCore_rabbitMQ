

using RabbitMQ.Client;

namespace WebAppOrder.Interfaces
{
    public interface IMyConnectionFactory
    {

        IModel GetChannel();

        void CreateConnection();

    }
}
