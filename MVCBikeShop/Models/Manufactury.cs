using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Manufactury
    {
        public Manufactury()
        {
            this.Bike = new HashSet<Bike>();
        }
        public int ManufacturyId { get; set; }
        public string ManufacturyTitle { get; set; }
        public virtual ICollection<Bike> Bike { get; set; }
    }
}
