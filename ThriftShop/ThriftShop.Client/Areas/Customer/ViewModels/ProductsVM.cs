using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.ViewModels
{
    public class ProductsVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
