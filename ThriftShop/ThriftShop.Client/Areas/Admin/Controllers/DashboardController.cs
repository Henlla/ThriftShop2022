using Microsoft.AspNetCore.Mvc;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
