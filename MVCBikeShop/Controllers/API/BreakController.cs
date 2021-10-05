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
    public class BreakController : Controller
    {
        private readonly BikeContext context;
        public BreakController(BikeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakType>>> Get()
        {
            return await context.BreakTypes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BreakType>> Get(int id)
        {
            BreakType breakType = await context.BreakTypes.FirstOrDefaultAsync(x => x.BreakTypeId == id);
            if (breakType == null)
            {
                return NotFound();
            }
            return Ok(breakType);
        }
        [HttpPost]
        public async Task<ActionResult<BreakType>> Post(BreakType breakType)
        {
            context.BreakTypes.Add(breakType);
            await context.SaveChangesAsync();
            return Ok(breakType);
        }

        [HttpPut]
        public async Task<ActionResult<Material>> Put(BreakType breakType)
        {
            context.Update(breakType);
            await context.SaveChangesAsync();
            return Ok(breakType);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BreakType>> Delete(int id)
        {
            BreakType breakType = await context.BreakTypes.FirstOrDefaultAsync(x => x.BreakTypeId == id);
            if (breakType == null)
            {
                return NotFound();
            }
            context.BreakTypes.Remove(breakType);
            await context.SaveChangesAsync();
            return Ok(breakType);
        }

    }
}
