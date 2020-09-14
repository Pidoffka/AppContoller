using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class EventInfo
    {
        [Key]
        [Required]
        public string phoneNumberCreator { get; set; }
        [Key]
        [Required]
        public string dateCreated { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int gender { get; set; }
        [Required]
        public int personNumber { get; set; }
        [Required]
        public string meetingDate { get; set; }
        [Required]
        public int userRegistred { get; set; }
        [Required]
        public bool Done { get; set; }

    }
}
