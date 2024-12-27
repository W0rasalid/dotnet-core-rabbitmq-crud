using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Consumer
{
    public interface IRabbitMQConsumer
    {
        public void ReceiveWeatherMessage();
        public void ReceiveLogMessage();

        //public Task<string> ReceiveMessage(string queueName);
        //public async string ReceiveMessage(string queueName);
    }
}
