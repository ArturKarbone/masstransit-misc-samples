using System;
using System.Threading.Tasks;
using FireOnWheels.Messaging;
using FireOnWheels.Registration.Service.Messages;
using MassTransit;

namespace FireOnWheels.Registration.Service
{
    public class RegisterOrderCommandConsumer: IConsumer<IRegisterOrder>
    {
        public async Task Consume(ConsumeContext<IRegisterOrder> context)
        {
            var command = context.Message;

            var id = 12;

            await Console.Out.WriteLineAsync($"Order with id {id} registered");

            await context.Publish<IOrderRegistered>(new OrderRegisteredEvent(command, id));
        }
    }
}
