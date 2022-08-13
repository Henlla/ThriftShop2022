using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using ThriftShop.Models;
using ThriftShop.Models.PageModel;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private string urlCategories = "https://localhost:7061/api/Categories/";
        private string urlProducts = "https://localhost:7061/api/Products/";
        private string urlColor = "https://localhost:7061/api/Colors/";
        private string urlSize = "https://localhost:7061/api/Sizes/";
        private HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            var modelCategories = JsonConvert.DeserializeObject<IEnumerable<Category>>(client.GetStringAsync(urlCategories).Result);
            var modelColors = JsonConvert.DeserializeObject<IEnumerable<Color>>(client.GetStringAsync(urlColor).Result);
            var modelSize = JsonConvert.DeserializeObject<IEnumerable<Size>>(client.GetStringAsync(urlSize).Result);
            //var modelProductType = JsonConvert.DeserializeObject<IEnumerable<ProductType>>(client.GetStringAsync(urlProductType).Result);
            ViewBag.category = modelCategories;
            ViewBag.color = modelColors;
            ViewBag.size = modelSize;
            //ViewBag.productType = modelProductType;
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product,int[] size, int[] color)
        {
            List<int> lSize = new List<int>();
            foreach (var i in size) {
                lSize.Add(i);
            }
            List<int> lColor = new List<int>();
            foreach (var i in color)
            {
                lColor.Add(i);
            }
            ProductVM productVm = new ProductVM
            {
                Product = product,
                //listSize = lSize,
                //listColor = lColor
            };
            var model = client.PostAsJsonAsync<ProductVM>(urlProducts, productVm).Result;
            return View();
        }

        public IActionResult ViewProduct() {
            var modelProduct = JsonConvert.DeserializeObject<IEnumerable<Product>>(client.GetStringAsync(urlProducts).Result);
            return View(modelProduct);
        }
    }
}
