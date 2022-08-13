using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username,string password)
        {

            return View();
        }
    }
}
