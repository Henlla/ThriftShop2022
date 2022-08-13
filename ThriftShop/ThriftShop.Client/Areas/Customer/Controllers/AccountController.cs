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
        [HttpGet]
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
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Setting()
        {
            var claim = (ClaimsIdentity)User.Identity;
            var id = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
            var info = JsonConvert.DeserializeObject<UserInfo>(client.GetStringAsync(urlUserInfo + id).Result);
            var account = JsonConvert.DeserializeObject<UserAccount>(client.GetStringAsync(urlUserAccount + info.AccountID).Result);
            AccountCollection model = new AccountCollection
            {
                UserInfo = info,
                UserAccount = account
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Setting(AccountCollection accountCollection)
        {
            UserAccount account = new UserAccount
            {
                Password = accountCollection.UserAccount.Password,
                AccountID = accountCollection.UserAccount.AccountID
            };
            UserInfo info = new UserInfo
            {
                Name = accountCollection.UserInfo.Name,
                Gender = accountCollection.UserInfo.Gender,
                Phone = accountCollection.UserInfo.Phone,
                Email = accountCollection.UserInfo.Email,
                Address = accountCollection.UserInfo.Address,
                City = accountCollection.UserInfo.City,
                PostalCode = accountCollection.UserInfo.PostalCode,
                State = accountCollection.UserInfo.State,
                UserId = accountCollection.UserInfo.UserId
            };
            var result2 = client.PutAsJsonAsync(urlUserInfo, info).Result;
            var result = client.PutAsJsonAsync(urlUserAccount, account).Result;
            return RedirectToAction("Setting");
        }
    }
}
