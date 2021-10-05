using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models.ViewModels
{
    public class BikeViewModel
    {
        public SelectList Manufacturies { get; set; }
        public SelectList Types { get; set; }
        public SelectList WheelDiameters { get; set; }
        public SelectList Materials { get; set; }
        public SelectList SpeedCount { get; set; }
        public SelectList Size { get; set; }
        public SelectList BreakType { get; set; }
        public List<Bike> Bikes { get; set; }
        //_______________________________________________________________ //Delete it
        public List<Manufactury> ManufacturiesList { get; set; }
        public List<Type> TypesList { get; set; }
        public List<Material> MaterialsList { get; set; }
        public List<BreakType> BreakTypeList { get; set; }
        //_______________________________________________________________//Delete it
    }
}
