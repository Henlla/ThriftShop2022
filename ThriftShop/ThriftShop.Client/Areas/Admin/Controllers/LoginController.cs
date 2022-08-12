using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ThriftShop.Client.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class LoginController : Controller
    {
        string url = "https://localhost:7061/api/Admin/";
        HttpClient client = new HttpClient();
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(ThriftShop.Models.Admin ad)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<ThriftShop.Models.Admin>(client.GetStringAsync(url + ad.Username + "/" + ad.Password).Result);
                var claim = new List<Claim>();
                claim.Add(new Claim(ClaimTypes.Name, ad.Username));
                claim.Add(new Claim(ClaimTypes.NameIdentifier, ad.Id.ToString()));
                var claimIdentify = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentify);
                await HttpContext.SignInAsync(claimPrincipal);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(ThriftShop.Models.Admin ad)
        {
            try
            {
                var accResult = client.PostAsJsonAsync(url, ad).Result;
                if (accResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
