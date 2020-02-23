using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestWeb_Api.Models
{
    public class Criterions_Age
    {
        [Key]
        [Required]
        public int Id_Team { get; set; }
        [Required]
        public int Age { get; set; }
        public virtual Team_to_Event Teams_To_Events { get; set; }

    }
}
