using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Data_Assess_Layer.Models
{
    [Index(nameof(Email), Name = "IX_Unique_Email", IsUnique = true)]
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Firstname { get; set; }

        public string? Lastname { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } // Navigation property for many-to-many relationship with Role
    }
    
}
