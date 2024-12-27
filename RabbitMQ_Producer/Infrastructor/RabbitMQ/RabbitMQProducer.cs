using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;

namespace RabbitMQ_Producer.Infrastructor.RabbitMQ
{
    public class RabbitMQProducer: IRabbitMQProducer
    {

        private readonly ConnectionFactory _factory;
        public RabbitMQProducer()
        {
            // Here we specify the Rabbit MQ Server. We use rabbitmq docker image and use it
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672, // Default RabbitMQ port
                UserName = "user", // Default username
                Password = "password"  // Default password
            };
        }

        public void SendWeatherMessage<T>(T message)
        {
            SendMessage("weather", message);
        }

        public void SendLogMessage<T>(T message)
        {
            SendMessage("log", message);
        }

        public async void SendMessage<T>(string queueName, T message)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };


            // Create the RabbitMQ connection using connection factory details as mentioned above
            using var connection = await _factory.CreateConnectionAsync();
            // Here we create channel with session and model
            using var channel = await connection.CreateChannelAsync();
            // Declare the queue after mentioning name and a few properties related to that
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            // Serialize the message
            var json = JsonConvert.SerializeObject(message, settings);
            var body = Encoding.UTF8.GetBytes(json);
            // Put the data onto the specified queue
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
            Console.WriteLine($" [x] Sent {message}");
        }

    }
}
