using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Type
    {
        public Type()
        {
            this.Bike = new HashSet<Bike>();
        }
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public virtual ICollection<Bike> Bike { get; set; }
    }
}
