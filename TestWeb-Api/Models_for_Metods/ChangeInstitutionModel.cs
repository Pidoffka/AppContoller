using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestWeb_Api.Models_for_Metods
{
    public class ChangeInstitutionModel
    {
        public int id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string placement { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
