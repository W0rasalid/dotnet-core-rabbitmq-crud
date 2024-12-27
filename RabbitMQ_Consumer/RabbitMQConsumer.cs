using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_Consumer.Repositories;
using RabbitMQ_Consumer.Services;
using SharedLibrary;
using SharedLibrary.Models.DTOs;

namespace RabbitMQ_Consumer
{
    internal class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly LogSystemService _logSystemService;
        private ApplicationDbContext _context;

        public RabbitMQConsumer(ApplicationDbContext context, LogSystemService logSystemService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logSystemService = logSystemService ?? throw new ArgumentNullException(nameof(logSystemService));

            // Here we specify the Rabbit MQ Server. We use rabbitmq docker image and use it
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672, // Default RabbitMQ port
                UserName = "user", // Default username
                Password = "password"  // Default password
            };
        }

        public void ReceiveWeatherMessage()
        {
            ReceiveMessage("weather");
        }

        public void ReceiveLogMessage()
        {
            ReceiveMessage("log");
        }

        public async void ReceiveMessage(string queueName)
        {
            // Create the RabbitMQ connection
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // config the queue
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            var tcs = new TaskCompletionSource<string>();

            // Event handler for received messages
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var jsonMessage = JsonConvert.SerializeObject(new { Message = message });

                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonMessage);
                if (jsonObject != null && jsonObject.TryGetValue("Message", out var messageObj))
                {
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(messageObj.ToString());
                    if (obj != null && obj.TryGetValue("Id", out var idObj))
                    {
                        var logId = idObj.ToString();
                        var log = await _context.logSystems.FirstOrDefaultAsync(o => o.Id == logId);
                        Console.WriteLine($" [x] Update Message Id: {logId}");
                        if (log != null)
                        {
                            log.IsSuccess = true;
                            log.Reference = "Updated";
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}
