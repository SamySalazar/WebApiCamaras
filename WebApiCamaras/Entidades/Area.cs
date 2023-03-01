namespace WebApiCamaras.Entidades
{
    public class Area
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string coordenadas { get; set; }
        public string dimensiones { get; set; }
        public string nivielRiesgo { get; set; }
        public List<Camara> camaras { get; set; }
    }
}
