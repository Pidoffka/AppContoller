using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class BadCategoryBuilding
    {
        [Key]
        [Required]
        public int badCategoryId { get; set; }
        [Key]
        [Required]
        public int buildingId { get; set; }
    }
}
