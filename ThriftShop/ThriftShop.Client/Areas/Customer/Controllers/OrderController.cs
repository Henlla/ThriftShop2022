using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private string ShopingCartUrl = "https://localhost:44320/api/ShoppingCart/";
        private string OrderDetailUrl = "https://localhost:44320/api/OrderDetails/";
        private string OrderUrl = "https://localhost:44320/api/Order/";
        private string CouponUrl = "https://localhost:44320/api/Coupon/";
        HttpClient httpClient = new HttpClient();
        public OrderController()
        {

        }
        [Area("Customer")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Customer")]
        [AllowAnonymous]
        public IActionResult CheckOut()
        {
            //int sessionId;
            ////get userid
            //var claim = (ClaimsIdentity)User.Identity;
            //if (claim.FindFirst(ClaimTypes.NameIdentifier) != null)
            //{ sessionId = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value); }
            //else { sessionId = 1; }

            // var model = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(ShopingCartUrl+"GetAll/"+sessionId).Result);
            var model = getShoppingcart();
            if (model != null)
            { return View(model); }
            else
            {
                return RedirectToAction("LoginUser", "Account");
            }

        }
        [Area("Customer")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult CheckCoupon(string couponCode)
        {
            Coupon useCoupon = null;
            var model = JsonConvert.DeserializeObject<IEnumerable<Coupon>>(httpClient.GetStringAsync(CouponUrl).Result);
            foreach (var item in model)
            {
             
                if (item.Code == couponCode)
                { useCoupon = item;
                    ViewBag.couponI = useCoupon.CouponId;
                    ViewBag.couponV = useCoupon.DiscountValue;
                    ViewBag.couponT = useCoupon.CouponType;
                    break;
                }
                else {
                    useCoupon = null;                
                }
                
            }
         
            var spCart=getShoppingcart();
            if (spCart != null)
            { 
                
                return View("CheckOut", spCart); }
            else
            {
                return RedirectToAction("LoginUser", "Account");
            }
           
        }



        //get shpping cart by userid
        public IEnumerable<ShoppingCart> getShoppingcart()
        {
            try
            {
                IEnumerable<ShoppingCart> model;

                int sessionId;
                //get userid
                var claim = (ClaimsIdentity)User.Identity;
                if (claim.FindFirst(ClaimTypes.NameIdentifier) != null)
                { sessionId = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value); }
                //test id=1
                else
                {
                    sessionId = 1;
                    //return null;
                }

                model = JsonConvert.DeserializeObject<IEnumerable<ShoppingCart>>(httpClient.GetStringAsync(ShopingCartUrl + "GetAll/" + sessionId).Result);


                return model;
            }
            catch (Exception)
            {

                return null;
            }
        }
        [Area("Customer")]
        public IActionResult Test(string Name,string Address,string City,string State,string PostalCode,string PhoneNumber,string CouponId)
        {
            try
            {
                int sessionId;
                var claim = (ClaimsIdentity)User.Identity;
                if (claim.FindFirst(ClaimTypes.NameIdentifier) != null)
                { sessionId = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value); }
                //test id=1
                else
                {
                    sessionId = 1;
                    //return null;
                }
                Order order = new Order()
                {
                    Name = Name,
                    Address = Address,
                    City = City,
                    State = State,
                    PostalCode = PostalCode,
                    PhoneNumber = PhoneNumber,
                    CouponId = int.Parse(CouponId),
                    UserId= sessionId

                };
                var model = httpClient.PostAsJsonAsync<Order>(OrderUrl, order).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Create", "OrderDetail");
                }
                else
                {
                    return RedirectToAction("CheckOut");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("CheckOut");
            }
          
          
        }

    }
}
