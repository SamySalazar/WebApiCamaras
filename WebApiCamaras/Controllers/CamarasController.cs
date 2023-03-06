using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using WebApiCamaras.Entidades;

namespace WebApiCamaras.Controllers
{
    [ApiController]
    [Route("api/camaras")]
    public class CamarasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CamarasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Camara>>> GetAll()
        {
            return await dbContext.Camaras.Include(x => x.area).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Camara>> GetById (int id)
        {
            return await dbContext.Camaras.FirstOrDefaultAsync(x => x.id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post (Camara camara)
        {
            var existArea = await dbContext.Areas.AnyAsync(x => x.id == camara.areaId);

            if (!existArea)
            {
                return BadRequest($"No existe el Área con el id: {camara.areaId}");
            }

            dbContext.Add(camara);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put (Camara camara, int id)
        {
            var exist = await dbContext.Camaras.AnyAsync(x => x.id == camara.id);

            if (!exist)
            {
                return NotFound("La camara especificada no existe.");
            }
            if (camara.id != id)
            {
                return BadRequest("El id de la camara no coincide con el establecido en la url.");
            }

            dbContext.Update(camara);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete (int id)
        {
            var exist = await dbContext.Camaras.AnyAsync(x => x.id == id);

            if (!exist)
            {
                return NotFound("E Recurso no fue econtrado.");
            }

            // var validateRelation = await dbContext.CamaraArea.AnyAsync

            dbContext.Remove(new Camara { id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
