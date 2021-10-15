using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DormitoryAlliance.Client.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("Users")] // Rename in sql server 'Users' -> 'AspNetUsers'
    public class ApplicationUser : IdentityUser
    {
        [PersonalData, Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        
        [PersonalData, Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
