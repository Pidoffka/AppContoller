using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class Event
    {
        [Key]
        [Required]
        public string phoneNumberCreator { get; set; }
        [Key]
        [Required]
        public string dateCreated { get; set; }
    }
}
