using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCBikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        BikeContext context;
        public AdminController(BikeContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Bikes.ToList());
        }
        public IActionResult Create(int? id)
        {
            ViewBag.DB = context;
            if (id != null)
            {
                var bikeToEdit = context.Bikes.Find(id);
                return View(bikeToEdit);
            }
            else
            {
                Bike bike = new Bike
                {
                    BikeId = 0,
                    BikeTitle = "",
                    ManufacturyId = 0,
                    TypeId = 0,
                    MaterialId = 0,
                    SpeedCount=0,
                    Size=0,
                    WheelDiameter=0,
                    BreakTypeId=0,
                    PhotoPath="",
                    Price=0
                };
                return View(bike);
            }
        }
        [HttpPost]
        public IActionResult Remove(int? bikeId)
        {
            context.Bikes.Remove(context.Bikes.Find(bikeId));
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Create(Bike bike)
        {
            if (bike.BikeId == 0)
            {
                context.Bikes.Add(bike);
            }
            else
            {
                var bikeToEdit = context.Bikes.FirstOrDefault(x => x.BikeId == bike.BikeId);
                bikeToEdit.BikeTitle = bike.BikeTitle;
                bikeToEdit.ManufacturyId = bike.ManufacturyId;
                bikeToEdit.TypeId = bike.TypeId;
                bikeToEdit.MaterialId = bike.MaterialId;
                bikeToEdit.SpeedCount = bike.SpeedCount;
                bikeToEdit.Size = bike.Size;
                bikeToEdit.WheelDiameter = bike.WheelDiameter;
                bikeToEdit.BreakTypeId = bike.BreakTypeId;
                bikeToEdit.PhotoPath = bike.PhotoPath;
                bikeToEdit.Price = bike.Price;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
