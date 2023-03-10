using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApiCamaras.Entidades
{
    public class Camara
    {
        public int id { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public string resolucion { get; set; }
        public string tipo { get; set; }
        public string descripcion{ get; set; }
        public bool estado { get; set; }
        public int idArea { get; set; }
        public Area area { get; set; }
    }
}
