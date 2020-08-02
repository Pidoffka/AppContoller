using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class CommentsInstitutions
    {
        [Key]
        [Required]
        public int Id_user { get; set; }
        [Key]
        [Required]
        public int id_institution { get; set; }

    }
}
