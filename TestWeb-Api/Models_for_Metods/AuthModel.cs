using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TestWeb_Api.Controllers
{
    public class AuthModel
    {
        public string Phone_Number { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Date_of_Birthday { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        
    }
}