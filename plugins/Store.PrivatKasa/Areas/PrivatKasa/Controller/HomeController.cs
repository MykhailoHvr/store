using Microsoft.AspNetCore.Mvc;
using Store.PrivatKasa.Areas.PrivatKasa.Models;

namespace Store.PrivatKasa.Areas.PrivatKasa.Controllers
{
    [Area("PrivatKasa")]
    public class HomeController : Controller
    {
        public IActionResult Index(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri,
            };

            return View(model);
        }

       
        public IActionResult Callback(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri,
            };



            return View(model);
        }
    }
}