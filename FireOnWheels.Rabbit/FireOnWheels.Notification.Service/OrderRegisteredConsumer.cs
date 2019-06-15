using System;
using FireOnWheels.Messaging;
using FireOnWheels.Notification.Messages;

namespace FireOnWheels.Notification
{
    public class OrderRegisteredConsumer
    {
        public void Consume(IOrderRegistered registered)
        {
            //Send notification to user
            Console.WriteLine($"Customer notification sent: Order id {registered.OrderId} registered");
        }
    }
}
