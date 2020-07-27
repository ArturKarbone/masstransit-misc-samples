using MassTransit;
using MassTransit.RabbitMqTransport;
using Polly;
using System;

namespace FireOnWheels.Contracts
{
    public static class BusExtensions
    {
        public static void StartSafely(this IBusControl bus)
        {

            var policy = Policy
               .Handle<RabbitMqConnectionException>()
               .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(20)
                });

            policy.Execute(() => bus.Start());

        }
    }
}
