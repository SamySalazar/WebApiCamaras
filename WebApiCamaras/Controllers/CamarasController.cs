using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApiCamaras.Entidades;

namespace WebApiCamaras.Controllers
{
    [ApiController]
    [Route("api/camaras")]
    public class CamarasController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Camara>> Get()
        {
            return new List<Camara>()
            {
                new Camara()
                {
                    id = 1,
                    modelo = "IPC-C120-D",
                    marca = "HIKVISION",
                    resolucion = "1920x1080",
                    tipo = "Interior",
                    descripcion = "Lente de 2.0 mm. Audio de 2 Vías. Detección de movimiento. Alimentación 5 VDC, interfase mirco USB.",
                    estado = true
                },
                new Camara()
                { 
                    id = 2, 
                    modelo = "IPC-B121H", 
                    marca = "HIKVISION", 
                    resolucion = "1920x1080",
                    tipo = "Exterior",
                    descripcion = "Lente de 2.8 mm. Detección de movimiento. Alimentación 12 VDC.",
                    estado = true 
                },
                new Camara()
                {
                    id = 3,
                    modelo = "DS-2CD2443G2-I",
                    marca = "HIKVISION",
                    resolucion = "2688x1520",
                    tipo = "Interior",
                    descripcion = "Lente de 2.8 mm. Audio de 2 Vías. Detección de movimiento. Alimentación 12 VDC.",
                    estado = true
                }
            };
        }
    }
}
