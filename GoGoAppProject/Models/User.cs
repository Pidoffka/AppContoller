using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class User
    {
        [Key]
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string dateSource { get; set; }
    }
}
