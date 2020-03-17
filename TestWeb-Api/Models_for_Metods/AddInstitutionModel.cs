﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models_for_Metods
{
    public class AddInstitutionModel
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Placement { get; set; }
        public string? Image_Institution { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
