using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThriftShop.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string? CategoryName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
    }
}
