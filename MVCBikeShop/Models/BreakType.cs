using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class BreakType
    {
        public BreakType()
        {
            this.Bike = new HashSet<Bike>();
        }
        public int BreakTypeId { get; set; }
        public string BreakTypeTitle { get; set; }
        public virtual ICollection<Bike> Bike { get; set; }
    }
}
