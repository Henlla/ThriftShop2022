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
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ValidateNever]
        public UserInfo UserInfo { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public string? Comment { get; set; }
    }
}
