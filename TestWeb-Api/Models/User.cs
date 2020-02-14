using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class User
    {
        //PIDARAS VOLODYA
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public int? FamilyId { get; set; }
        public string JsonToken { get; set; }
        public virtual Family Family { get; set; }
    }
}
