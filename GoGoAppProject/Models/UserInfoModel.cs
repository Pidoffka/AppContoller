using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class UserInfoModel
    {
        [Key]
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public int gender { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string birthday { get; set; }
        [Required]
        public string avatar { get; set; }
        [Required]
        public string json { get; set; }
        [Required]
        public string nickname { get; set; }
    }
}
