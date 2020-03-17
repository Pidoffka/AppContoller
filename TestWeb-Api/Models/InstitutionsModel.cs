using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class InstitutionsModel
    {
        [Key]
        [Required]
        public int Id_Institution { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Placement { get; set; }
        public string? Image_Institution { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        
    }
}
