using System;

namespace FireOnWheels.Messaging
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri =
            "amqp://guest:guest@localhost:5672/";
        public const string JsonMimeType =
            "application/json";

        public static string GetRegisterOrderExchange() => GetExchangeName(typeof(IRegisterOrder));
        public static string GetRegisterOrderQueue(string service) => GetQueueName(typeof(IRegisterOrder), service);

        public static string GetOrderRegisteredExchange() => GetExchangeName(typeof(IOrderRegistered));
        public static string GetOrderRegisteredQueue(string service) => GetQueueName(typeof(IOrderRegistered), service);

        private static string GetExchangeName(Type type) => type.FullName;
        private static string GetQueueName(Type type, string service) => $"{type.FullName}_{service}";
    }
}
