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
        private string urlCoupon = "https://localhost:7061/api/Coupon/";
        private HttpClient client = new HttpClient();

        [Route("Admin/Home")]
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
            var sizes = JsonConvert.DeserializeObject<IEnumerable<Size>>(client.GetStringAsync(urlSize).Result);
            var colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(client.GetStringAsync(urlColor).Result);

            List<Size> lSize = new List<Size>();
            foreach (var i in size) {
                string sizename = sizes.Where(s => s.SizeId == i).FirstOrDefault().SizeType;
                Size _size = new Size()
                {
                    SizeId = i,
                    SizeType = sizename
                };
                lSize.Add(_size);
            }
            List<Color> lColor = new List<Color>();
            foreach (var i in color)
            {
                string colorname = colors.Where(c => c.ColorId == i).FirstOrDefault().ColorType;
                Color _color = new Color()
                {
                    ColorId = i,
                    ColorType = colorname
                };
                lColor.Add(_color);
            }
            ProductVM productVm = new ProductVM
            {
                Product = product,
                Size = lSize,
                Color = lColor
            };
            var model = client.PostAsJsonAsync<ProductVM>(urlProducts, productVm).Result;
            ViewBag.success = "Create product success";
            return RedirectToAction("ViewProduct");
        }

        [Route("Admin/Product")]
        public IActionResult ViewProduct() {
            var modelProduct = JsonConvert.DeserializeObject<IEnumerable<Product>>(client.GetStringAsync(urlProducts+"GetAll/").Result);
            return View(modelProduct);
        }

        //return View List of Categories
        [Route("Admin/Category")]
        public IActionResult ViewCategory()
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(client.GetStringAsync(urlCategories).Result);
            return View(categories);
        }

        //View Create Category
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        //Post Category
        [HttpPost]
        public IActionResult CreateCategory(Category newCategory)
        {
            var category = client.PostAsJsonAsync<Category>(urlCategories, newCategory).Result;
            ViewBag.success = "Create Category Success";
            return RedirectToAction("ViewCategory");
        }

        //View list Sizes
        [Route("Admin/Size")]
        public IActionResult ViewSize()
        {
            var sizes = JsonConvert.DeserializeObject<IEnumerable<Size>>(client.GetStringAsync(urlSize).Result);
            return View(sizes);
        }
        //View Create Size
        [HttpGet]
        public IActionResult CreateSize()
        {
            return View();
        }
        //Post Size
        [HttpPost]
        public IActionResult CreateSize(Size newSize)
        {
            var size = client.PostAsJsonAsync<Size>(urlSize, newSize).Result;
            if (size != null)
            {
                ViewBag.success = "Create Size Success";
                return RedirectToAction("ViewSize");
            }
            else
            {
                ViewBag.error = "Create Size Failed";
                return View();
            }
        }


        //View List Colors
        [Route("Admin/Color")]
        public IActionResult ViewColor()
        {
            var colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(client.GetStringAsync(urlColor).Result);
            return View(colors);
        }
        //View Create Color
        [HttpGet]
        public IActionResult CreateColor()
        {
            return View();
        }
        //Post Color
        [HttpPost]
        public IActionResult CreateColor(Color newColor)
        {
            var color = client.PostAsJsonAsync<Color>(urlColor, newColor).Result;
            if (color != null)
            {
                ViewBag.success = "Create Color Success";
                return RedirectToAction("ViewColor");
            }
            else
            {
                ViewBag.error = "Create Color Failed";
                return View();
            }
        }


        //View List Coupons
        [Route("Admin/Coupon")]
        public IActionResult ViewCoupon()
        {
            var colors = JsonConvert.DeserializeObject<IEnumerable<Coupon>>(client.GetStringAsync(urlCoupon).Result);
            return View(colors);
        }
        //View Create Coupons
        [HttpGet]
        public IActionResult CreateCoupon()
        {
            return View();
        }
        //Post Color
        [HttpPost]
        public IActionResult CreateCoupon(Coupon newCoupon)
        {
            var color = client.PostAsJsonAsync<Coupon>(urlCoupon, newCoupon).Result;
            if (color != null)
            {
                ViewBag.success = "Create Coupon Success";
                return RedirectToAction("ViewCoupon");
            }
            else
            {
                ViewBag.error = "Create Coupon Failed";
                return View();
            }
        }
    }
}
