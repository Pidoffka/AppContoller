using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestWeb_Api.Models
{
    public class Friend: Follower
    {
        
        [Required]
        public int Id_User_Sender { get; set; }
        
        [Required]
        public int Id_User_Receiver { get; set; }
        [Required]
        public bool Checked { get; set; }
        [Required]
        public bool Viewed { get; set; }
        [Required]
        public bool Answer { get; set; }

        public virtual User Users1 { get; set; }
        public virtual User Users2 { get; set; }
    }
}
