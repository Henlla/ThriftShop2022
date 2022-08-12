using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.Client.Areas.Customer.Models.AccountModel;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private HttpClient client = new HttpClient();
        string urlUserAccount = "https://localhost:7061/api/UserAcccount/";
        string urlUserInfo = "https://localhost:7061/api/User/";

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet()]
        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(UserAccount userAccount)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<UserAccount>(client.GetStringAsync(urlUserAccount + userAccount.Username + "/" + userAccount.Password).Result);
                var claim = new List<Claim>();
                claim.Add(new Claim(ClaimTypes.Name, userAccount.Username));
                claim.Add(new Claim(ClaimTypes.NameIdentifier, model.AccountID.ToString()));
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
        public IActionResult RegisterUser(AccountCollection accountCollection, string cfp)
        {
            try
            {
                if (accountCollection.UserAccount.Password.Equals(cfp))
                {

                    var accResult = client.PostAsJsonAsync(urlUserAccount, accountCollection.UserAccount).Result;
                    if (accResult.IsSuccessStatusCode)
                    {
                        var infoResult = client.PostAsJsonAsync(urlUserInfo, accountCollection.UserInfo).Result;
                        if (infoResult.IsSuccessStatusCode)
                        {
                            return RedirectToAction("LoginUser");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.msg = "Confirm password wrong";
                    return View();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Setting()
        {
            var claim = (ClaimsIdentity)User.Identity;
            var id = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
            var model = JsonConvert.DeserializeObject<UserInfo>(client.GetStringAsync(urlUserInfo + id).Result);
            return View(model);
        }
        [HttpPost]
        public IActionResult Setting(AccountCollection accountCollection)
        {
            return View();
        }
    }
}
