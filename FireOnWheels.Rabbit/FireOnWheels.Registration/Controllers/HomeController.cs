using System;
using System.Threading.Tasks;
using FireOnWheels.Messaging;
using FireOnWheels.Registration.Messages;
using FireOnWheels.Registration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MassTransit;

namespace FireOnWheels.Registration.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult RegisterOrder()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public IActionResult RegisterOrder(OrderViewModel model)
        {
            var registerOrderCommand = new RegisterOrder(model);

            //Send RegisterOrderCommand
            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.SendRegisterOrderCommand(registerOrderCommand);
            }

            return View("Thanks");
        }
    }
}
