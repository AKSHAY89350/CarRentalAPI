using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer.Models
{
    
    public class UserRole
    {
        // Foreign key referencing User.
        public int UserId { get; set; }

        // Navigation property to User.
        public Users Users { get; set; }

        // Foreign key referencing Role.
        public int RoleId { get; set; }

        // Navigation property to Role.
        public Role Role { get; set; }
    }
    
}
