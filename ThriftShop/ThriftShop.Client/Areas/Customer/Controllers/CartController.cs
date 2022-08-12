using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private string urlShoppingCart = "https://localhost:7061/api/ShoppingCart/";
        private HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            var objCart = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(client.GetStringAsync(urlShoppingCart).Result);
            return View(objCart);
        }

        public IActionResult CheckOut()
        {
            return View();
        }


    }
}
