using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class MessageModel
    {
        [Key]
        [Required]
        public string phoneNumberSender { get; set; }
        [Key]
        [Required]
        public string phoneNumberReceiver { get; set; }
    }
}
