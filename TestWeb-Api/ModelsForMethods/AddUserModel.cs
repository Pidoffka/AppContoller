using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.ModelsForMethods
{
    public class AddUserModel
    {
        public string phoneNumber { get; set; }
        public string dateSource { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int gender { get; set; }
        public string city { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public string birthday { get; set; }
    }
}
