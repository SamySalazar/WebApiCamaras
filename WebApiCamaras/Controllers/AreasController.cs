using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCamaras.Entidades;

namespace WebApiCamaras.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreasController : ControllerBase
    {
        private ApplicationDbContext dbContext;

        public AreasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Area>>> Get()
        {
            return await dbContext.Areas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Area area)
        {
            dbContext.Add(area);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/areas/1
        public async Task<ActionResult> Put (Area area, int id)
        {
            if (area.id != id)
            {
                return BadRequest("El id del area no coincide con el establecido en la url");
            }

            var exist = await dbContext.Areas.AnyAsync(x => x.id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Update(area);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete (int id)
        {
            var exist = await dbContext.Areas.AnyAsync(x => x.id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Area() 
            { 
                id = id 
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
