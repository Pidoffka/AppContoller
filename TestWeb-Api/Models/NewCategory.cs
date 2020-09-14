using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class NewCategory
    {
        [Key]
        [Required]
        public int newCategoryId { get; set; }

        [Required]
        public string dateSource { get; set; }
    }
}
