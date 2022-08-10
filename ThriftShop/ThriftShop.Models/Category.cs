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
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
    }
}
