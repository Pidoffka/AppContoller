using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class BuildingInfoModel
    {
        [Key]
        [Required]
        public int buildingId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public bool isClose { get; set; }
        [Required]
        public double rating { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string workTime { get; set; }
        [Required]
        public string avgPrice { get; set; }
        [Required]
        public string website { get; set; }
    }
}
