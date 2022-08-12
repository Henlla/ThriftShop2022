using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private string productUrl = "https://localhost:7061/api/Products/";
        HttpClient httpClient = new HttpClient();
        
        public IActionResult Index()
        {
            var product = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(productUrl).Result);
            return View(product);
        }
        
        public IActionResult Details(int productId)
        {
            return View();
        }


        public IActionResult Compare()
        {
            return View();
        }

        public IActionResult WishList()
        {
            return View();
        }
    }
}
