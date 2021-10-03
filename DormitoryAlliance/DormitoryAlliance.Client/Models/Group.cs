using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models
{
    [Table("Groups")]
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required, Column(TypeName = "nvarchar(15)")]
        public string Name { get; set; }
    }
}