using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.Mq.Publishers
{
    public static class Publisher<T>
    {
        public static void Publish(T tamsEvent, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" , UserName = "admin" , Password = "omsgd@2017"};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tamsEvent));

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: properties,
                body: body);
        }

        
    }
}
