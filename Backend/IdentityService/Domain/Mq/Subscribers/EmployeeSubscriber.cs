using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Domain.Mq.Subscribers
{
    public static class EmployeeSubscriber<T>
    {
        public static void Subscribe(string queueName, Action<T> myMethodName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            T data = default;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    data = JsonConvert.DeserializeObject<T>(message);
                }
                catch (Exception)
                {
                    // ignored
                }

                // call calculation, or do whatever you want with the data
                if (data != null && !data.Equals(default))
                {
                    myMethodName(data);
                }

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

        }
    }
}
