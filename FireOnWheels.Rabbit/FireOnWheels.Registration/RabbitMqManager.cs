using System;
using System.Text;
using FireOnWheels.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FireOnWheels.Registration
{
    public class RabbitMqManager : IDisposable
    {
        private readonly IModel channel;
        public RabbitMqManager()
        {
            var connectionFactory =
                new ConnectionFactory { Uri = new Uri(RabbitMqConstants.RabbitMqUri) };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }
        public void SendRegisterOrderCommand(IRegisterOrder command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.GetRegisterOrderExchange(),
                type: ExchangeType.Direct);

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType =
                RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(
                exchange: "",
                routingKey: RabbitMqConstants.GetRegisterOrderQueue("Registration"),
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
