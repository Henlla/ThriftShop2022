using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models
{
    public class Color_Product
    {
        [Key]
        public int ColorId { get; set; }
        [Key]
        public int? ProductId { get; set; }

        [ForeignKey("ColorId")]
        public Color Color { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
