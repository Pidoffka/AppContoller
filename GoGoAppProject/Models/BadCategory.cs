using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class BadCategory
    {
        [Key]
        [Required]
        public int badCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string dateSource { get; set; }
    }
}
