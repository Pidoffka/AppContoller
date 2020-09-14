using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class BadCategory
    {
        [Key]
        [Required]
        int badCategoriesId { get; set; }
        [Required]
        string Name { get; set; }
        [Required]
        string dateSource { get; set; }
    }
}
