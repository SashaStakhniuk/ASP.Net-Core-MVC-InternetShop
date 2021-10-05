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
    public class TypeController:Controller
    {
            private readonly BikeContext context;
            public TypeController(BikeContext context)
            {
                this.context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Models.Type>>> Get()
            {
                return await context.Types.ToListAsync();
            }
            [HttpGet("{id}")]
            public async Task<ActionResult<Models.Type>> Get(int id)
            {
            Models.Type type = await context.Types.FirstOrDefaultAsync(x => x.TypeId == id);
                if (type == null)
                {
                    return NotFound();
                }
                return Ok(type);
            }
            [HttpPost]
            public async Task<ActionResult<Models.Type>> Post(Models.Type type)
            {
                context.Types.Add(type);
                await context.SaveChangesAsync();
                return Ok(type);
            }

            [HttpPut]
            public async Task<ActionResult<Models.Type>> Put(Models.Type type)
            {
                context.Update(type);
                await context.SaveChangesAsync();
                return Ok(type);
            }
            [HttpDelete("{id}")]
            public async Task<ActionResult<Models.Type>> Delete(int id)
            {
            Models.Type type = await context.Types.FirstOrDefaultAsync(x => x.TypeId == id);
                if (type == null)
                {
                    return NotFound();
                }
                context.Types.Remove(type);
                await context.SaveChangesAsync();
                return Ok(type);
            }
        
    }
}
