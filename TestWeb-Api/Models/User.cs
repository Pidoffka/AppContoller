using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
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
