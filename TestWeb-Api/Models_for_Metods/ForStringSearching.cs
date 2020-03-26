using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models_for_Metods
{
    public class ForStringSearching
    {
        public int Id_Institution { get; set; }
        public List<string> Titles { get; set; }
        public int Count_Coincidences { get; set; }
    }
}
