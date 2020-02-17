using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class Team_to_Event
    {
        [Key]
        [Required]
        public int Id_Team { get; set; }
        [Required]
        public int Id_Creator { get; set; }
        [Required]
        public int Id_Event { get; set; }
        [Required]
        public int Number_of_people { get; set; }
        [Required]
        public byte Open_Closed {get; set;}
        public string Password { get; set; }
        public virtual User Users { get; set; }
        public virtual Event_Institution Event_Institutions { get; set; }
        public virtual ICollection<Criterions_Gender> Criterions_Genders { get; set; }
        public virtual ICollection<Criterions_Age> Criterions_Ages { get; set; }
    }
}
