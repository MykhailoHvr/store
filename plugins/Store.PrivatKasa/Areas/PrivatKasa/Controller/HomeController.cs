using Microsoft.AspNetCore.Mvc;

namespace Store.PrivatKasa.Areas.PrivatKasa.Controllers
{
    [Area("PrivatKasa")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // /YandexKassa/Home/Callback
        public IActionResult Callback()
        {
            return View();
        }
    }
}