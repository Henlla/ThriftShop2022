using System.ComponentModel.DataAnnotations;

namespace ThriftShop.API
{
    public class EmailModel
    {
        public string To { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
    }
}
