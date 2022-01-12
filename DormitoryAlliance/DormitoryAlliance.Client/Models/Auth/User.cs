using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models.Auth
{
    [Table("users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
