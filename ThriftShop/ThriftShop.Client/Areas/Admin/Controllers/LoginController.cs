using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class LoginController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

        public IActionResult CreateAcc()
        {
            return View();
        }

        
    }
}
