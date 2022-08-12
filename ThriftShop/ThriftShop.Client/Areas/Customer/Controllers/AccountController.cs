using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
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
