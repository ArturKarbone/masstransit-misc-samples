using FireOnWheels.Registration.ViewModels;
using FireOnWheels.Registration.Messages;
using Microsoft.AspNetCore.Mvc;

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
            var registerOrderCommand = new RegisterOrderCommand(model);

            //Send RegisterOrderCommand
            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.SendRegisterOrderCommand(registerOrderCommand);
            }

            return View("Thanks");
        }
    }
}