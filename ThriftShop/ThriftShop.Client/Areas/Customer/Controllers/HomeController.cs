﻿using Microsoft.AspNetCore.Authorization;
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
        string urlSendMail = "https://localhost:7061/api/Email/";
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
            //Kiem tra dang ki chua
            var claim = (ClaimsIdentity)User.Identity;
            var claims = claim.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null)
            {
                var model = JsonConvert.DeserializeObject<UserInfo>(client.GetStringAsync(urlUserInfo + int.Parse(claims.Value)).Result);
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
            return View();
        }
        [HttpPost]
        public IActionResult ContractUs(ContractMail contractMail)
        {
            EmailModel mailSend = new EmailModel
            {
                From = contractMail.InfoUser.Email,
                To = "thriftshop@gmail.com",
                Subject = contractMail.EmailModel.Subject,
                Body = contractMail.EmailModel.Body
            };
            var result = client.PostAsJsonAsync<EmailModel>(urlSendMail, mailSend).Result;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.mess = "Send success";
            }
            else
            {
                ViewBag.mess = "Check information again";
            }
            return RedirectToAction("ContactUs");
        }
    }
}
