using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models.PageModel
{
     public class SpCart_Order_ODetail
    {
       public IEnumerable<ShoppingCart>? ShoppingCart { get; set; }
       public Order? Order { get; set; }
       public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
