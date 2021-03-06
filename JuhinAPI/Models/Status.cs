using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Status
    {
        [Key]
        [Required]
        public int StatusId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        //Status 1-m Delivery
        public List<Delivery> Deliveries { get; set; }
    }
}
