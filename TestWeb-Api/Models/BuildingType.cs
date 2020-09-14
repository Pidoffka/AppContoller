using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class BuildingType
    {
        [Key]
        [Required]
        public int buildingId { get; set; }
        [Required]
        public string title { get; set; }
    }
}
