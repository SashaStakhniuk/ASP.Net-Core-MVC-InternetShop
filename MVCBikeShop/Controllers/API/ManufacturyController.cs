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
    [Route("api/[controller]")]
    public class ManufacturyController:Controller
    {
        private readonly BikeContext context;
        public ManufacturyController(BikeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufactury>>> Get()
        {
            return await context.Manufacturies.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufactury>> Get(int id)
        {
            Manufactury manufactury = await context.Manufacturies.FirstOrDefaultAsync(x => x.ManufacturyId == id);
            if (manufactury == null)
            {
                return NotFound();
            }
            return Ok(manufactury);
        }
        [HttpPost]
        public async Task<ActionResult<Manufactury>> Post(Manufactury manufactury)
        {
            context.Manufacturies.Add(manufactury);
            await context.SaveChangesAsync();
            return Ok(manufactury);
        }
      
        [HttpPut]
        public async Task<ActionResult<Manufactury>> Put(Manufactury manufactury)
        {
            context.Update(manufactury);
            await context.SaveChangesAsync();
            return Ok(manufactury);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Manufactury>> Delete(int id)
        {
            Manufactury manufactury = await context.Manufacturies.FirstOrDefaultAsync(x => x.ManufacturyId == id);
            if (manufactury == null)
            {
                return NotFound();
            }
            context.Manufacturies.Remove(manufactury);
            await context.SaveChangesAsync();
            return Ok(manufactury);
        }
    }
}
