using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class UndergroundBuildingModel
    {
        [Key]
        [Required]
        public string name { get; set; }
        [Key]
        [Required]
        public int buildingId { get; set; }
    }
}
