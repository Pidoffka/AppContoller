﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models_for_Metods
{
    public class DeleteMessageModel
    {
        public int Id_User_Reader { get; set; }
        public int Id_User_Sender { get; set; }
        public DateTime Date_time_send { get; set; }
    }
}
