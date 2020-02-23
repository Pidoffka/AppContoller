using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    
    public class Connection_Event_Category
    {
        [Key]
        [Required]
        public int Id_Event { get; set; }
        [Key]
        [Required]
        public int Id_Categories { get; set; }
        
        public virtual Category Categories { get; set; }
        public virtual Event_Institution Events_Institutions { get; set; }


    }
}
