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
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, 999999)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
        public int? ColorId { get; set; }
        [ForeignKey("ColorId")]
        [ValidateNever]
        public Color Color { get; set; }
        public int? SizeId { get; set; }
        [ForeignKey("SizeId")]
        [ValidateNever]
        public Size Size { get; set; }
        public int? ProductTypeId { get; set; } // nam nu, unisex
        [ForeignKey("ProductTypeId")]
        [ValidateNever]
        public ProductType ProductType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } // ngay tao san pham
        public IEnumerable<ProductImage> ProductImage { get; set; }

    }
}
