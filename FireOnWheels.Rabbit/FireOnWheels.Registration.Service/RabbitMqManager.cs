using System;
using System.Text;
using FireOnWheels.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using FireOnWheels.Registration.Service;


namespace FireOnWheels.Registration
{
    public class RabbitMqManager : IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory =
                new ConnectionFactory { Uri = RabbitMqConstants.RabbitMqUri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForRegisterOrderCommand()
        {
            channel.QueueDeclare(
                queue: RabbitMqConstants.GetRegisterOrderQueue("Registration"),
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1,
                global: false);

            var consumer = new RegisteredOrderCommandConsumer(this);
            channel.BasicConsume(
                queue: RabbitMqConstants.GetRegisterOrderQueue("Registration"),
                noAck: false,
                consumer: consumer);
        }

        public void SendOrderRegisteredEvent(IOrderRegistered command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.GetOrderRegisteredExchange(),
                type: ExchangeType.Fanout);
            channel.QueueDeclare(
                queue: RabbitMqConstants.GetOrderRegisteredQueue("Notification"),
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: RabbitMqConstants.GetOrderRegisteredQueue("Notification"),
                exchange: RabbitMqConstants.GetOrderRegisteredExchange(),
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstants.GetOrderRegisteredExchange(),
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
