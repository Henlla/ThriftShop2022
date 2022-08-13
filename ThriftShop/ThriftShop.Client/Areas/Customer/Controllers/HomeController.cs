using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ThriftShop.API;
using ThriftShop.Client.Areas.Customer.Models.MailSendContract;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        string productUrl = "https://localhost:7061/api/Products/";
        string feedbackUrl = "https://localhost:7061/api/FeedBack/";
        string urlUserInfo = "https://localhost:7061/api/User/";
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            //var pro = JsonConvert.DeserializeObject<IEnumerable<Product>>(client.GetStringAsync(productUrl + "GetAll/").Result);
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            ContractMail contractMail;
            var claim = (ClaimsIdentity)User.Identity;
            var id = int.Parse(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (id != null)
            {
                var model = JsonConvert.DeserializeObject<UserInfo>(client.GetStringAsync(urlUserInfo + id).Result);
                EmailModel mailModel = new EmailModel
                {
                    From = model.Email,
                    To = String.Empty,
                    Subject = String.Empty,
                    Body = String.Empty
                };
                contractMail = new ContractMail
                {
                    InfoUser = model,
                    EmailModel = mailModel,
                };
                return View(contractMail);
            }
            else
            {
                contractMail = new ContractMail();
                return View(contractMail);
            }
        }
        [HttpPost]
        public IActionResult ContractUs(ContractMail contractMail)
        {
            return View();
        }

    }
}
