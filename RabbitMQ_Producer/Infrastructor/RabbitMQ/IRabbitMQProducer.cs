namespace RabbitMQ_Producer.Infrastructor.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendWeatherMessage<T>(T message);
        public void SendLogMessage<T>(T message);
    }
}
