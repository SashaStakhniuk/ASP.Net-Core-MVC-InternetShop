using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Bike
    {
        public int BikeId { get; set; }
        public string BikeTitle { get; set; }
        public int ManufacturyId { get; set; }
        public int TypeId { get; set; }
        public int MaterialId { get; set; }
        public int SpeedCount { get; set; }
        public int Size { get; set; }
        public double WheelDiameter { get; set; }
        public int BreakTypeId { get; set; }
        public string PhotoPath { get; set; }
        public decimal Price { get; set; }
        public virtual Manufactury Manufactury { get; set; }
        public virtual Type Type { get; set; }
        public virtual Material Material { get; set; }
        public virtual BreakType BreakType { get; set; }

    }
}
