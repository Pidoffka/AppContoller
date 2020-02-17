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
        public int Id_User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        
        public DateTime? Date_of_Birthday { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Phone_Number { get; set; }
        
        [Required]
        public string Password { get; set; }
  
        public string JsonToken { get; set; }
        public string Avatar { get; set; }
        public virtual ICollection<Team_to_Event> Teams_to_Events { get; set; }
        
    }
}
