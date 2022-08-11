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
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        //khang
        //[ForeignKey("OrderId")]
        //[ValidateNever]
        //public Order OrderHeader { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; } // Gias cua san pham

    }
}
