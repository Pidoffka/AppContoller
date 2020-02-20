using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestWeb_Api.Models
{
    public class Friend
    {
        [Key]
        [Required]
        public int Id_User_Sender { get; set; }
        [Required]
        public string? Phone_Number_Sender { get; set; }
        [Key]
        [Required]
        public int Id_User_Receiver { get; set; }
        [Required]
        public string Phone_Number_Receiver { get; set; }
        [Required]
        public bool Answer { get; set; }
        public bool Checked { get; set; }

        //public virtual ICollection<User> Users { get; set; }
        
    }
}
