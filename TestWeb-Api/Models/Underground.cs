using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class Underground
    {
        [Key]
        [Required]
        public string name { get; set; }
        [Required]
        public string color { get; set; }
    }
}
