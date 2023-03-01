using Microsoft.AspNetCore.Mvc;
using WebApiCamaras.Entidades;

namespace WebApiCamaras.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreasController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Area>> Get()
        {
            return new List<Area>()
            {
                new Area()
                {
                    id = 1,
                    nombre = "Vestíbulo",
                    descripcion = "Habitación inmediata a la puerta principal",
                    coordenadas = "41°24'12.2\"N",
                    dimensiones = "5m x 6m",
                    nivielRiesgo = "Bajo",
                    camaras = new List<Camara>
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
                    }
                }
            };
        }
    }
}
