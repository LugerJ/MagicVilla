using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1, 
                    Nombre="Villa Zirahuen",
                    Detalle="Lago Hermoso",
                    ImageUrl="",
                    Ocupantes=1000,
                    MetrosCuadrados=5000,
                    Tarifa=500,
                    Amenidad="Nadar",
                    FechaCreacion=DateTime.Now,
                    FechaActualizacion=DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa Tarejero",
                    Detalle = "Rio Hermoso",
                    ImageUrl = "",
                    Ocupantes=300,
                    MetrosCuadrados = 1000,
                    Tarifa = 200,
                    Amenidad = "Nadar contracorriente",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
                );
        }
    }
}
