using System;
using System.Threading.Tasks;
using FireOnWheels.Messaging;
using FireOnWheels.Registration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FireOnWheels.Registration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RegisterOrder()
        {
            var random = Guid.NewGuid().ToString().Substring(0, 4);

            return View(new OrderViewModel()
            {
                PickupName = $"Pickup Name {random}",
                PickupAddress = $"Pickup Address {random}",
                PickupCity = $"Picup City {random}",
                DeliverName = $"Delivery Name {random}",
                DeliverAddress = $"Delivery Address {random}",
                DeliverCity = $"Delivery City {random}",
                Fragile = true,
                Oversized = false,
                Weight = 100
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderViewModel model)
        {
            //Send RegisterOrderCommand
            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" +
                $"{RabbitMqConstants.RegisterOrderServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);

            await endPoint.Send<IRegisterOrder>(new
            {
                PickupName = model.PickupName,
                PickupAddress = model.PickupAddress,
                PickupCity = model.PickupCity,
                DeliverName = model.DeliverName,
                DeliverAddress = model.DeliverAddress,
                DeliverCity = model.DeliverCity,
                Weight = model.Weight,
                Fragile = model.Fragile,
                Oversized = model.Oversized
            });
            return View("Thanks");
        }
    }
}
