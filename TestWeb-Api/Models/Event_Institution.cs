using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class Event_Institution
    {
        [Key]
        [Required]
        public int Id_Event { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date_Event { get; set; }
        [Required]
        public string Placement { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Connection_Event_Category> Connection_Event_Categories { get; set; }
        public virtual ICollection<Team_to_Event> Teams_to_Events { get; set; }
    }
}
