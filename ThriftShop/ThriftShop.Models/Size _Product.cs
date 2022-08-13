using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public string SizeType { get; set; }


    }
}
