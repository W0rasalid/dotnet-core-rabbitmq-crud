using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace rabbitMQ.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
