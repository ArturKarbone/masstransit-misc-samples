namespace FireOnWheels.Messaging
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://rabbit/fireonwheels/";       
        public const string UserName = "fireonwheels";
        public const string Password = "fireonwheels";
        public const string RegisterOrderServiceQueue = "registerorder.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string FinanceServiceQueue = "finance.service";
    }
}
