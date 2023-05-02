using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApiCamaras.Entidades
{
    public class Camara
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Resolucion { get; set; }
        public string Tipo { get; set; }
        public string Descripcion{ get; set; }
        public bool Estado { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
