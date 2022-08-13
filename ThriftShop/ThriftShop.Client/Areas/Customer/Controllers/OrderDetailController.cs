using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.Models;
using ThriftShop.Models.PageModel;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    public class OrderDetailController : Controller
    {
        private string ShopingCartUrl = "https://localhost:7061/api/ShoppingCart/";
        private string OrderDetailUrl = "https://localhost:7061/api/api/OrderDetails/";
        private string OrderUrl = "https://localhost:7061/api/Order/";
        private string CouponUrl = "https://localhost:7061/api/Coupon/";

        HttpClient httpClient = new HttpClient();
        public OrderDetailController()
        {

        }
        [Area("Customer")]
        [HttpGet]
        public IActionResult CreateOrderDetail(Order json)
        {

            //shopping cart
            var spCart = getShoppingcart();
            if (spCart != null)
            {
                SpCart_Order_ODetail modelView = new SpCart_Order_ODetail();

                modelView.ShoppingCart = spCart;
                modelView.Order = json;
                double total = 0;
                double discount = 0;
              
                Coupon myCoupon = new Coupon() ;
                if (json.CouponId != null) { myCoupon = JsonConvert.DeserializeObject<Coupon>(httpClient.GetStringAsync(CouponUrl + json.CouponId).Result); }
                else { myCoupon = null; }
                if (myCoupon != null)
                {
                    foreach (var item in spCart)
                    {
                        total += (double)item.Product.FinalPrice * item.Count;
                    }

                    if (myCoupon.CouponType == "Cash")
                    {
                        discount = (double)myCoupon.DiscountValue;

                    }
                    else
                    {
                        discount = (double)(total * myCoupon.DiscountValue / 100);
                    }
                    total = total - discount;

                    ViewBag.amount = total;
                    ViewBag.discount = discount;
                    if (myCoupon.Quantity > 0)
                    {
                        myCoupon.Quantity -= 1;
                    }
                    else {

                        return RedirectToAction("CheckOut", "Order");
                    }
                    var UCoupon = httpClient.PutAsJsonAsync<Coupon>(CouponUrl, myCoupon).Result;
              

                }



                return View(modelView);
            }
            else
            {
                return RedirectToAction("LoginUser", "Account");
            }
        }
        //var model = JsonConvert.DeserializeObject<IEnumerable<Coupon>>(httpClient.GetStringAsync(CouponUrl).Result);

        [Area("Customer")]
        [HttpPost]

        //order 
        public IActionResult NewOrder(SpCart_Order_ODetail obj)
        {
            Order myOrder = obj.Order;
            
            IEnumerable<ShoppingCart> spCartList = obj.ShoppingCart;
            //myOrder = obj.Order;
            List<OrderDetail> list = new List<OrderDetail>();
            foreach (var item in spCartList)
            {
                OrderDetail orderDetail = new OrderDetail();
               
                orderDetail.ProductId=item.ProductId;
                orderDetail.OrderId = myOrder.Id;
                orderDetail.Price = item.Product.FinalPrice;
                orderDetail.Count = item.Count;
              
              var model=  httpClient.PostAsJsonAsync<OrderDetail>(OrderDetailUrl, orderDetail).Result;
                if (model.IsSuccessStatusCode)
                {
                   httpClient.DeleteAsync(ShopingCartUrl+item.Id);
                }
                
            }
            //IEnumerable<Order>


         
          



            return RedirectToAction("Index","Home");
        }    //

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
    }
}

