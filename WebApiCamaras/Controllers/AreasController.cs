﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using WebApiCamaras.Entidades;
using WebApiCamaras.Filtros;
using WebApiCamaras.Services;

namespace WebApiCamaras.Controllers
{
    [ApiController]
    [Route("api/areas")]
    // [Authorize]
    // Ruta: api/[controller] -> [controller] toma el prefijo del nombre del controller
    public class AreasController : ControllerBase
    {
        private ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<AreasController> logger;

        public AreasController(
            ApplicationDbContext context,
            IService service,
            ServiceTransient serviceTransient,
            ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton,
            ILogger<AreasController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        //[ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AreasControllerTransient = serviceTransient.Guid,
                ServiceA_Transient = service.GetTransient(),
                AreasControllerScoped = serviceScoped.Guid,
                ServiceA_Scoped = service.GetScoped(),
                AreasControllerSingleton = serviceSingleton.Guid,                                
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet] // Ruta: api/areas
        [HttpGet("listado")] // Ruta: api/areas/listado
        [HttpGet("/listado")] // Ruta: listado
        [ServiceFilter(typeof(FiltroDeAccion))]
        //[ResponseCache(Duration = 10)]
        // [Authorize]
        public async Task<ActionResult<List<Area>>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Estamos obteniendo las areas");
            logger.LogWarning("Este es un mensaje de prueba Warning");
            return await dbContext.Areas.Include(x => x.Camaras).ToListAsync();
        }

        // Tipos de Datos de retorno: Tipo específico, ActionResult<T>, IActionResult
        // Retorno de dato específico
        //public List<Area> Get()
        //{
        //    return dbContext.Areas.Include(x => x.Camaras).ToList();
        //}
        // Tipo de dato de retorno ActionResult<T>; Se puede retornar Action result o el tipo de dato T
        //public ActionResult<Area> Get(int id, string param)
        //{
        //    var area = dbContext.Areas.FirstOrDefault(x => x.Id == id);

        //    if (area == null)
        //    {
        //        return NotFound();
        //    }
        //    return area;
        //}
        // Tipo de dato de retorno IActinoResult: No se puede retornar otro tipo de dato
        //public IActionResult Get(int id, string param)
        //{
        //    var area = dbContext.Areas.FirstOrDefault(x => x.Id == id);

        //    if (area == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(area);
        //}

        [HttpGet("primero")] // api/areas/primero?nombre=vestibulo&descripcion=hola
        public async Task<ActionResult<Area>> PrimerArea([FromHeader] int valor, [FromQuery] string nombre)
        {
            return await dbContext.Areas.FirstOrDefaultAsync();
        }

        // Donde id, es una variable de ruta, e int, es la restricción de la variable esperada
        // Ruta: api/areas/{id:int}
        // También se puede añadir más parámetros en la ruta.
        // Ej. "{id:int}/{parametro2}" o con valor por defecto parametro2=porDefecto
        [HttpGet("{id:int}/{param?}")]
        public async Task<ActionResult<Area>> Get(int id, string param)
        {
            var area = await dbContext.Areas.FirstOrDefaultAsync(x => x.Id == id);

            if (area == null)
            {
                return NotFound();
            }
            return area;
        }

        // También otra alternativa para la variable puede ser: {nombre}
        // Cambiando la variable en la función
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Area>> Get([FromRoute] string nombre)
        {
            var area = await dbContext.Areas.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (area == null)
            {
                return NotFound();
            }
            return area;
        }

        // Fuentes de datos de los parametros de una petición Ej. [FromBody]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Area area)
        {
            var existsAreaName = await dbContext.Areas.AnyAsync(x => x.Nombre == area.Nombre);

            if (existsAreaName)
            {
                return BadRequest($"Ya existe un área con el nombre {area.Nombre}");
            }

            dbContext.Add(area);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/areas/1
        public async Task<ActionResult> Put (Area area, int id)
        {
            if (area.Id != id)
            {
                return BadRequest("El id del area no coincide con el establecido en la url");
            }

            var exist = await dbContext.Areas.AnyAsync(x => x.Id == id);
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
            var exist = await dbContext.Areas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Area() 
            { 
                Id = id 
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
