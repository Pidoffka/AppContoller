using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TestWeb_Api.Models
{
    public class CategoriesModel
    {
        [Key]
        [Required]
        public int Id_Category { get; set; }
        [Required]
        public string Name_Category { get; set; }

    }
}
