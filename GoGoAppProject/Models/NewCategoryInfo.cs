using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class NewCategoryInfo
    {
        [Key]
        [Required]
        public int newCategoryId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string image { get; set; }
    }
}
