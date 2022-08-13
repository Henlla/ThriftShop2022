using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        string urlC = "https://localhost:7061/api/Coupon/";
      
        HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        //create

        [HttpGet]
        public IActionResult CreateCoupon()
        {
          
            return View();
        }

        [HttpPost]

        public IActionResult CreateCoupon(Coupon obj)
        {
            try
            {
                var model = httpClient.PostAsJsonAsync<Coupon>(urlC, obj).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Falied");

            }
            return View();

           
        }
        //update
        [HttpGet]
        public IActionResult Update(int coupon)
        {
            var model = JsonConvert.DeserializeObject<Coupon>(httpClient.GetStringAsync(urlC + "/"+ coupon).Result);
            return View(model);
        }


        [HttpPost]
        public IActionResult Update(Coupon obj)
        {
            try
            {
                var model = httpClient.PutAsJsonAsync<Coupon>(urlC, obj).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int coupon)
        {
            try
            {
                var model = httpClient.DeleteAsync(urlC + coupon).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "failed");
            }
            return RedirectToAction("Index");
        }
       

    }
}
