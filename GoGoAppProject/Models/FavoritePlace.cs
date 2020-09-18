using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.Models
{
    public class FavoritePlace
    {
        [Key]
        [Required]
        public int buildingId { get; set; }
        [Key]
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public int attribute { get; set; }
    }
}
