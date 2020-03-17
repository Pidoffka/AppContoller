using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class MarksInstitutionsModel
    {
        [Key]
        [Required]
        public int Id_User { get; set; }
        [Key]
        [Required]
        public int Id_Institution { get; set; }
        [Required]
        public int Mark { get; set; }
    }
}
