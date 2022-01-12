using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryAlliance.Client.Models
{
    [Table("rooms")]
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public int Number { get; set; }

        public int DormitoryId { get; set; }
        
        [ForeignKey("DormitoryId")]
        public Dormitory Dormitory { get; set; }
    }
}