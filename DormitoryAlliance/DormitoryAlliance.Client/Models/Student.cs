using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models
{
    [Table("Students")]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Patronymic { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int Course { get; set; }
    }
}