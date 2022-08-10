using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
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
    }
}
