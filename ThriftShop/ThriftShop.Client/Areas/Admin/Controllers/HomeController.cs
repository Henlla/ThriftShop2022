using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }

        public IActionResult CreateProduct()
        {
            return View();
        }
    }
}
