using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class MessageModel
    {
        [Key]
        [Required]
        public int Id_User_Sender { get; set; }
        
        [Key]
        [Required]
        public int Id_User_Receiver { get; set; }
        
        [Required]
        public string Text { get; set; }
        [Required]
        public bool Checked { get; set; }
        [Required]
        public bool Viewed { get; set; }
        [Required]
        public string Date_time_send { get; set; }
        
    }
}
