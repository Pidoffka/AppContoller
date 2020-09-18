using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class UserReviewModel
    {
        [Key]
        [Required]
        public string phoneNumberSender { get; set; }
        [Key]
        [Required]
        public string phoneNumberReceiver { get; set; }
        [Required]
        public string review { get; set; }
        [Required]
        public bool commend { get; set; }
    }
}
