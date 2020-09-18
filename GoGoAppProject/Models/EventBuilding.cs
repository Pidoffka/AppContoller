using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class EventBuilding
    {
        [Key]
        [Required]
        public int buildingId { get; set; }
        [Key]
        [Required]
        public string phoneNumberCreator { get; set; }
        [Key]
        [Required]
        public string dateCreated { get; set; }
    }
}
