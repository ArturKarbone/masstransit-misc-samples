using System;
using FireOnWheels.Contracts;
using FireOnWheels.Messaging;
using MassTransit;

namespace FireOnWheels.Finance.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Finance";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.FinanceServiceQueue, e =>
                {
                    e.Consumer<OrderRegisteredConsumer>();
                });
            });
            
            bus.StartSafely();

            Console.WriteLine("Listening for Order registered events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();        }
    }
}
