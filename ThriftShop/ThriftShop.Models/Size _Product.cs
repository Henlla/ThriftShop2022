using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models
{
    public class Size_Product
    {
        [Key]
        public int SizeId { get; set; }
        [Key]
        public int? ProductId { get; set; }

        [ForeignKey("SizeId")]
        public Size Size { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
