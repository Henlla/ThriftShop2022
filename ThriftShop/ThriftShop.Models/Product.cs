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
        public string? SexType { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, 999999)]
        public double Price { get; set; }

        [Required]
        [Range(1, 100)]
        public int SalePercent { get; set; }

        [NotMapped]
        public double FinalPrice
        {
            get
            {
                return Price - (((double)SalePercent / 100) * Price);
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } // ngay tao san pham

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }

        //public int SizeId { get; set; }
        //[ForeignKey("SizeId")]
        [ValidateNever]
        public List<Size_Product> Size_Product { get; set; }

        [ValidateNever]
        public List<Color_Product> Color_Product { get; set; }

        [ValidateNever]
        public List<ProductImage> ProductImage { get; set; }

        [ValidateNever]
        [NotMapped]
        public List<string> ImageList { get; set; }

       
    }
}
