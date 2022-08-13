using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models.PageModel
{
    public class CartVM
    {
       public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        // public List<ShoppingCart> Cart { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
