using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurrentPages { get; set; }
    }
}
