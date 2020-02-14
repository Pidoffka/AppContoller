using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TestWeb_Api.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id_Categories { get; set; }
        [Required]
        public string Name_Categories { get; set; }
    }
}
