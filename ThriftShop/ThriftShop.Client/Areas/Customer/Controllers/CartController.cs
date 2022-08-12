using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private string ShopingCartUrl = "https://localhost:44320/api/ShoppingCart/";
        private string OrderDetailUrl = "https://localhost:44320/api/OrderDetails/";
        private string OrderUrl = "https://localhost:44320/api/Order/";
        HttpClient httpClient = new HttpClient();
   
        public CartController()
        {

        }

        [Area("Customer")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Customer")]
        public IActionResult CheckOut()
        {
            //int sessionId;
            ////get userid
            //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            //if (claim != null)
            //{
            //    sessionId = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
            //}
            //else { sessionId = 1992; }
            
     
           
            //var model = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(ShopingCartUrl).Result);

          
            return View(sessionId);
        }
        [Area("Customer")]
        public IActionResult Test()
        {

            return View();
        }

    }
}
