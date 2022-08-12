using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThriftShop.Client.Areas.Customer.Models.AccountModel;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private HttpClient client = new HttpClient();
        string urlUserAccount = "https://localhost:7061/api/UserAcccount/";
        string urlUserInfo = "https://localhost:7061/api/UserInfo/";
        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginUser(UserAccount userAccount)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<UserAccount>(client.GetStringAsync(urlUserAccount + userAccount.Username + "/" + userAccount.Password).Result);
                HttpContext.Session.SetString("userId", userAccount.AccountID.ToString());
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
    }
}
