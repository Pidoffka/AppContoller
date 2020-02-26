using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models_for_Metods
{
    public class UserWithStringDateTime
    {
        public int Id_User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Phone_Number { get; set; }
        public string Password { get; set; }
        public string JsonToken { get; set; }
    }
}
