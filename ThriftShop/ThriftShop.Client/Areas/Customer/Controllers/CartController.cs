using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ThriftShop.Models.PageModel;
using ThriftShop.Models.ModelVirtual;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {

        private HttpClient httpClient = new HttpClient();
        private string urlShoppingCart = "https://localhost:7061/api/ShoppingCart/";
        public CartController()
        {

        }


        public IActionResult Index()
        {
            //List<ShoppingCart> objCart = new List<ShoppingCart>();
            ShoppingCart shoppingCart = new ShoppingCart();
            CartVM cart = new CartVM
            {
                shoppingCarts = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(urlShoppingCart).Result),
                Cart = shoppingCart
            };
            return View(cart);
        }
        //[Area("Customer")]
        //[HttpPost]
        //public IActionResult Index(string[] productId, int[] delete)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    foreach (var itemCart in cartList) {
        //    //     var model = httpClient.PutAsJsonAsync<ShoppingCart>(urlShoppingCart, itemCart).Result;
        //    //    }
        //    //}
        //    return View();
        //}



        public IActionResult CheckOut()
        {
            return View();
        }

        #region API CALLs
        [HttpGet]
        public async Task<IActionResult> Plus(int cartId)
        {
            string action = "plus";
            var objCart = JsonConvert.DeserializeObject<ShoppingCart>(httpClient.GetStringAsync(urlShoppingCart + cartId).Result);
            objCart.Action = action;
            var model = await httpClient.PutAsJsonAsync<ShoppingCart>(urlShoppingCart, objCart);
            var response = await model.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<ShoppingCart>(response);
            var jsonShoppingCart = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(urlShoppingCart).Result);
            return Json(new { success = true, data = json, shoppingcart = jsonShoppingCart });
        }

        [HttpGet]
        public async Task<IActionResult> Minus(int cartId)
        {
            string action = "minus";
            var objCart = JsonConvert.DeserializeObject<ShoppingCart>(httpClient.GetStringAsync(urlShoppingCart + cartId).Result);
            objCart.Action = action;
            var model = await httpClient.PutAsJsonAsync<ShoppingCart>(urlShoppingCart, objCart);
            var response = await model.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<ShoppingCart>(response);
            var jsonShoppingCart = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(urlShoppingCart).Result);
            return Json(new { success = true, data = json, shoppingcart = jsonShoppingCart });
        }
        [HttpDelete]
        public IActionResult Delete(int cartId)
        {
            var model = httpClient.DeleteAsync(urlShoppingCart + cartId).Result;
            return Json(new { success = true });
        }
        #endregion
    }

}





