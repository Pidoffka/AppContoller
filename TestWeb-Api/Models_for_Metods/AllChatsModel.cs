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
        public string Avatar { get; set; }
        public int Id_User_sender { get; set; }
        public string Text_Message { get; set; }
        public int Count_Dont_Read { get; set; }
    }
}
