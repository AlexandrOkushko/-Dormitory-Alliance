using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models.Auth
{
    [Table("roles")]
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }
    }
}
