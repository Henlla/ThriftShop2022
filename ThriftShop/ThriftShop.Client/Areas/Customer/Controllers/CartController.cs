using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ThriftShop.Client.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
     
        HttpClient httpClient = new HttpClient();

        public CartController()
        {

        }

      
        public IActionResult Index()
        {
            return View();
        }
        
        
        public IActionResult CheckOut()
        {
            return View();
        }

        
    }
}

