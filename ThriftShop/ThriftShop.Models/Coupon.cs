using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftShop.Models
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouponId { get; set; }
        public string? Code { get; set; }
        public string? CouponType { get; set; }
        public double? DiscountValue { get; set; }
        public DateTime CreatedDate { get; } = DateTime.Now;
        public DateTime ExpiredDate { get; set; }
        public int Quantity { get; set; }
    }
}
