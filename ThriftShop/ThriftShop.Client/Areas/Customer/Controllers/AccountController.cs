using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        [Route("Login")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Setting(int accountId)
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
