using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BikeController : Controller
    {
        private readonly BikeContext context;
        public BikeController(BikeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> Get()
        {
            return await context.Bikes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> Get(int id)
        {
            Bike bike = await context.Bikes.FirstOrDefaultAsync(x => x.BikeId == id);
            if(bike==null)
            {
                return NotFound();
            }
            return Ok(bike);
        }
        [HttpPost]
        public async Task<ActionResult<Bike>> Post(Bike bike)
        {
            //Bike bikeToAdd = new Bike();
            //bikeToAdd.BikeId = bike.BikeId;
            //bikeToAdd.BikeTitle = bike.BikeTitle;
            //bikeToAdd.ManufacturyId = bike.ManufacturyId;
            //bikeToAdd.TypeId = bike.TypeId;
            //bikeToAdd.MaterialId = bike.MaterialId;
            //bikeToAdd.SpeedCount = bike.SpeedCount;
            //bikeToAdd.Size = bike.Size;
            //bikeToAdd.WheelDiameter = bike.WheelDiameter;
            //bikeToAdd.BreakTypeId = bike.BreakTypeId;
            //bikeToAdd.PhotoPath = bike.PhotoPath;
            //bikeToAdd.Price = bike.Price;// Decimal.Parse(bike.Price.ToString());
            //if (!string.IsNullOrEmpty(bike.BikeTitle) && !string.IsNullOrEmpty(bike.PhotoPath) && bike.Size != 0 && bike.SpeedCount != 0 && bike.WheelDiameter!=0 && bike.Price>=0)
            //{
            //    context.Bikes.Add(bike);
            //    await context.SaveChangesAsync();
            //    return Ok(bike);
            //}
            //else
            //{
            //    ModelState.AddModelError("DatasModelError", "Please,enter all required datas correctly(all numbers must be >= 0)");
            //    ModelState.AddModelError("TitleError", "Enter the title");
            //    return BadRequest(ModelState);
            //}

            if (string.IsNullOrEmpty(bike.BikeTitle))
            {
                ModelState.AddModelError("TitleError", "Enter the title");

            }
            if (string.IsNullOrEmpty(bike.PhotoPath))
            {
                ModelState.AddModelError("PhotoPathError", "Enter the photoPath");
            }
            if (bike.Size <= 0)
            {
                ModelState.AddModelError("BikeSizeError", "Size must be bigger than 0");
            }
            if (bike.SpeedCount <= 0)
            {
                ModelState.AddModelError("SpeedCountError", "Count of speeds must be bigger than 0");
            }
            if (bike.WheelDiameter <= 0)
            {
                ModelState.AddModelError("WheelDiameterError", "Wheels diameter must be bigger than 0");
            }
            if (bike.Price <= 0)
            {
                ModelState.AddModelError("PriceError", "Price must be bigger than 0");
            }
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("DatasModelError", "Please,enter all required datas correctly(all numbers must be >= 0)");
                return BadRequest(ModelState);
            }
            else
            {

                context.Bikes.Add(bike);
                await context.SaveChangesAsync();
                return Ok(bike);
            }
            //return View(bike);
        }
        //public ActionResult<Bike> Post(Bike bike)
        //{
        //    //Bike bikeToAdd = new Bike();
        //    //bikeToAdd.BikeId = bike.BikeId;
        //    //bikeToAdd.BikeTitle = bike.BikeTitle;
        //    //bikeToAdd.ManufacturyId = 1;//bike.ManufacturyId;
        //    //bikeToAdd.TypeId = 1;//bike.TypeId;
        //    //bikeToAdd.MaterialId = 1;// bike.MaterialId;
        //    //bikeToAdd.SpeedCount = bike.SpeedCount;
        //    //bikeToAdd.Size = bike.Size;
        //    //bikeToAdd.WheelDiameter = bike.WheelDiameter;
        //    //bikeToAdd.BreakTypeId = 1;// bike.BreakTypeId;
        //    //bikeToAdd.PhotoPath = bike.PhotoPath;
        //    //bikeToAdd.Price = bike.Price;// Decimal.Parse(bike.Price.ToString());

        //    context.Bikes.Add(bike);
        //    context.SaveChanges();
        //    return Ok(bike);
        //}
        [HttpPut]
        public async Task<ActionResult<Bike>> Put(Bike bike)
        {
            if (string.IsNullOrEmpty(bike.BikeTitle))
            {
                ModelState.AddModelError("TitleError", "Enter the title");

            }
            if (string.IsNullOrEmpty(bike.PhotoPath))
            {
                ModelState.AddModelError("PhotoPathError", "Enter the photoPath");
            }
            if (bike.Size <= 0)
            {
                ModelState.AddModelError("BikeSizeError", "Size must be bigger than 0");
            }
            if (bike.SpeedCount <= 0)
            {
                ModelState.AddModelError("SpeedCountError", "Count of speeds must be bigger than 0");
            }
            if (bike.WheelDiameter <= 0)
            {
                ModelState.AddModelError("WheelDiameterError", "Wheels diameter must be bigger than 0");
            }
            if (bike.Price <= 0)
            {
                ModelState.AddModelError("PriceError", "Price must be bigger than 0");
            }
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("DatasModelError", "Please,enter all required datas correctly(all numbers must be >= 0)");
                return BadRequest(ModelState);
            }
            else
            {

                context.Update(bike);
                await context.SaveChangesAsync();
                return Ok(bike);
            }
           
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bike>> Delete (int id)
        {
            Bike bike = await context.Bikes.FirstOrDefaultAsync(x => x.BikeId == id);
            if(bike==null)
            {
                return NotFound();
            }
            context.Bikes.Remove(bike);
            await context.SaveChangesAsync();
            return Ok(bike);
        }
    }
}
