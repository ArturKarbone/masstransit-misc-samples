using System;
using System.Threading.Tasks;
using FireOnWheels.Messaging;
using MassTransit;

namespace FireOnWheels.Finance.Service
{
    public class OrderRegisteredConsumer: IConsumer<IOrderRegistered>
    {
        public async Task Consume(ConsumeContext<IOrderRegistered> context)
        {
            //Save to db
            await Console.Out.WriteLineAsync($"New order received: Order id {context.Message.OrderId}");
        }
    }
}
