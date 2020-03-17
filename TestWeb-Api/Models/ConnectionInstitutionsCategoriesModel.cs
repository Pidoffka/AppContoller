using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class ConnectionInstitutionsCategoriesModel
    {
        [Key]
        [Required]
        public int Id_Institution { get; set; }
        [Key]
        [Required]
        public int Id_Category { get; set; }
    }
}
