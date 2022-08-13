using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Client.Areas.Customer.ViewModels;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private string categoryUrl = "https://localhost:7061/api/Categories/";
        private string productUrl = "https://localhost:7061/api/Products/";
        HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            ProductsVM productsVM = new ProductsVM
            {
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(productUrl+ "GetAll/").Result),
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(categoryUrl).Result)
            };
            return View(productsVM);
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
