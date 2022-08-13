using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.ClientModel
{
    public class ProductClientModel
    {

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Color> Colors { get; set; }
    }
}
