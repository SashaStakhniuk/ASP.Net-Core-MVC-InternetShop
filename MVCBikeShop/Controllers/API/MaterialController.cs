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
    public class MaterialController : Controller
    {
        private readonly BikeContext context;
        public MaterialController(BikeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> Get()
        {
            return await context.Materials.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> Get(int id)
        {
            Material material = await context.Materials.FirstOrDefaultAsync(x => x.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }
        [HttpPost]
        public async Task<ActionResult<Material>> Post(Material material)
        {
            context.Materials.Add(material);
            await context.SaveChangesAsync();
            return Ok(material);
        }

        [HttpPut]
        public async Task<ActionResult<Material>> Put(Material material)
        {
            context.Update(material);
            await context.SaveChangesAsync();
            return Ok(material);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Material>> Delete(int id)
        {
            Material material = await context.Materials.FirstOrDefaultAsync(x => x.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }
            context.Materials.Remove(material);
            await context.SaveChangesAsync();
            return Ok(material);
        }

    }
}
