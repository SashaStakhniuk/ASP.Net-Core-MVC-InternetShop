using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Material
    {
        public Material()
        {
            this.Bike = new HashSet<Bike>();
        }
        public int MaterialId { get; set; }
        public string MaterialTitle { get; set; }
        public virtual ICollection<Bike> Bike { get; set; }
    }
}
