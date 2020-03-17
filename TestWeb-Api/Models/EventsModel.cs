using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestWeb_Api.Models
{
    public class EventsModel
    {
        [Key]
        [Required]
        public int Id_Event { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Date_Event { get; set; }
        [Required]
        public string Placement { get; set; }
        public string? Image_Event { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [Required]
        public bool Opened { get; set; }
    }
}
