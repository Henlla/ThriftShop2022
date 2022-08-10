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
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool Gender { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public int AccountID { get; set; }
        [ForeignKey("AccountID")]
        [ValidateNever]
        public UserAccount? UserAccount { get; set; }
    }
}
