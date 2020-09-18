using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class UserInEvent
    {
        [Key]
        [Required]
        public string phoneNumber { get; set; }
        [Key]
        [Required]
        public string phoneNumberCreator { get; set; }
        [Key]
        [Required]
        public string dateCreated { get; set; }
        [Required]
        public bool takePart { get; set; }
    }
}
