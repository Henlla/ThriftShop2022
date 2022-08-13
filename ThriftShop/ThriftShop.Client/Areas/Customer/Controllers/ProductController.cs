using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Models;
using ThriftShop.Client.Areas.Customer.ClientModel;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private string categoryUrl = "https://localhost:7061/api/Categories/";
        private string productUrl = "https://localhost:7061/api/Products/";
        private string colorUrl = "https://localhost:7061/api/Colors/";
        HttpClient httpClient = new HttpClient();

        public IActionResult Index(string? keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                ProductClientModel productsVM = new ProductClientModel
                {
                    Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(productUrl + "GetAll/").Result),
                Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(categoryUrl).Result),
                    Colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(httpClient.GetStringAsync(colorUrl).Result),
                };
                return View(productsVM);
            }
            else
            {
                ProductClientModel productsVM = new ProductClientModel
                {
                    Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(httpClient.GetStringAsync(productUrl + "GetAll/").Result),
                    Categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(categoryUrl).Result),
                    Colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(httpClient.GetStringAsync(colorUrl).Result)
                };
                return View(productsVM);
            }

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
