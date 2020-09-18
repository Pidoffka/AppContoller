using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class Building
    {
        [Key]
        [Required]
        public int buildingId { get; set; }
        [Required]
        public string dateSource { get; set; }
    }
}
