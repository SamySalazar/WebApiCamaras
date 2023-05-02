namespace WebApiCamaras.Entidades
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Coordenadas { get; set; }
        public string Dimensiones { get; set; }
        public string NivielRiesgo { get; set; }
        public List<Camara> Camaras { get; set; }
    }
}
