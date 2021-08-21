using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}
