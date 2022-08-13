using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        private string ShopingCartUrl = "https://localhost:7061/api/ShoppingCart/";
        private string OrderDetailUrl = "https://localhost:7061/api/api/OrderDetails/";
        private string OrderUrl = "https://localhost:7061/api/Order/";
        private string CouponUrl = "https://localhost:7061/api/Coupon/";
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
            {
                double total = 0;
                foreach (var item in model)
                {
                   total += (double)item.Product.FinalPrice * item.Count;
                }
                ViewBag.amount = total;
                return View(model); }
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
            double discount = 0;
            double total = 0;
            foreach (var item in model)
            {
             
                if (item.Code == couponCode)
                { useCoupon = item;
                    ViewBag.couponI = useCoupon.CouponId;
                    ViewBag.couponV = useCoupon.DiscountValue;
                    ViewBag.couponT = useCoupon.CouponType;
                    ViewBag.couponC = useCoupon.Code;
                    break;
                }
                else {
                    useCoupon = null;                
                }

               
            }
          


            var spCart=getShoppingcart();
            if (spCart != null)
            {
                foreach (var item in spCart)
                {
                   total+= (double)item.Product.FinalPrice * item.Count; 
                }

                if (useCoupon.CouponType == "Cash")
                {
                    discount = (double)useCoupon.DiscountValue;

                }
                else
                {
                    discount = (double)(total * useCoupon.DiscountValue / 100);
                }
                total = total - discount;

                ViewBag.amount = total;
                ViewBag.discount = discount;

                return View("CheckOut", spCart);
            }
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
                    //sessionId = 1;
                    return null;
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
        public async Task<IActionResult> Test(string Name,string Address,string City,string State,string PostalCode,string PhoneNumber,string CouponId)
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
                    //sessionId = 1;
                    return null;
                }
                Order order = new Order()
                {
                    Name = Name,
                    Address = Address,
                    City = City,
                    State = State,
                    PostalCode = PostalCode,
                    PhoneNumber = PhoneNumber,
                    UserId = sessionId,
                   
                };



                if (CouponId != null)
                {
                    order.CouponId = int.Parse(CouponId);
                }
         

                //get model
                var model = await  httpClient.PostAsJsonAsync<Order>(OrderUrl,order);
                var respone =await model.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Order>(respone);
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("CreateOrderDetail", "OrderDetail",json);
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
