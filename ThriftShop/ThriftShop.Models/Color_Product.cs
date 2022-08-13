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
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int? ProductId { get; set; }

    }
}
