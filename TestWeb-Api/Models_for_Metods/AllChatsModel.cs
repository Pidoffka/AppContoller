using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestWeb_Api.Models_for_Metods
{
    public class AllChatsModel
    {
        public int Id_User { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Date_of_Birthday { get; set; }
        
        public string Gender { get; set; }
        
        public string Phone_Number { get; set; }

        
        public string Password { get; set; }
        
        public string Avatar { get; set; }
        
        public string JsonToken { get; set; }
        public int Id_User_sender { get; set; }
        public string Text_Message { get; set; }
    }
}
