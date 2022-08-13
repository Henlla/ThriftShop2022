using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models.PageModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Size> listSize { get; set; }
        public List<Size> listColor { get; set; }
    }
}
