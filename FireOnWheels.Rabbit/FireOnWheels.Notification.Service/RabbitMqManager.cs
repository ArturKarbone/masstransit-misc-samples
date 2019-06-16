﻿using System;
using System.Text;
using FireOnWheels.Messaging;
using FireOnWheels.Notification.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FireOnWheels.Notification
{
    public class RabbitMqManager: IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = RabbitMqConstants.RabbitMqUri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForOrderRegisteredEvent()
        {
            #region queue and qos setup

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

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
#endregion
            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;
                if (contentType != RabbitMqConstants.JsonMimeType)
                    throw new ArgumentException(
                        $"Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                var orderConsumer = new OrderRegisteredConsumer();
                var commandObj = 
                JsonConvert.DeserializeObject<OrderRegistered>(message);
                orderConsumer.Consume(commandObj);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, 
                    multiple: false);
            };
            channel.BasicConsume(
                queue: RabbitMqConstants.GetOrderRegisteredQueue("Notification"),
                noAck: false,
                consumer: eventingConsumer);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
