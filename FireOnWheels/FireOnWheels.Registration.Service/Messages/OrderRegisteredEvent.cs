using System;
using System.Runtime.InteropServices;
using FireOnWheels.Messaging;

namespace FireOnWheels.Registration.Service.Messages
{
    public class OrderRegisteredEvent : IOrderRegistered
    {
        private IRegisterOrder command;
        private Guid orderId;
        public OrderRegisteredEvent(IRegisterOrder command, Guid orderId)
        {
            this.command = command;
            this.orderId = orderId;
        }
        public Guid OrderId => orderId;
        public string PickupName => command.PickupName;
        public string PickupAddress => command.PickupAddress;
        public string PickupCity => command.PickupCity;

        public string DeliverName => command.DeliverName;
        public string DeliverAddress => command.DeliverAddress;
        public string DeliverCity => command.DeliverCity;

        public int Weight => command.Weight;
        public bool Fragile => command.Fragile;
        public bool Oversized => command.Oversized;
    }
}
