using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private string categoryUrl = "https://localhost:7061/api/Categories/";
        HttpClient httpClient = new HttpClient();
        
        public ProductController()
        {

        }
        public IActionResult Index()
        {
            var categories = this.GetCategories();
            return View(categories);
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

        public IEnumerable<Category> GetCategories()
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(httpClient.GetStringAsync(categoryUrl+ "GetAll").Result);
            return categories;
        }
    }
}
