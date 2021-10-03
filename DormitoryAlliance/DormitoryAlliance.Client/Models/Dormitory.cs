using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models
{
    [Table("Dormitories")]
    public class Dormitory
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }

        [Required]
        public int Floors { get; set; }
    }
}