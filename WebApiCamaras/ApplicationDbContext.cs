using Microsoft.EntityFrameworkCore;
using WebApiCamaras.Entidades;

namespace WebApiCamaras
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Camara> Camaras { get; set; }        
    }
}
