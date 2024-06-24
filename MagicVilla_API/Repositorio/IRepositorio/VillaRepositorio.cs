using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public VillaRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }

        public List<string> ErrorMessages { get; private set; }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            try
            {
                // entidad.FechaActualizacion = DateTime.Now;


                //_db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                //_db.Attach(entidad);
              
                _db.Villas.Update(entidad);
                await _db.SaveChangesAsync();
                return entidad;
            }
            catch (Exception ex)
            {

                ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                  
                return entidad;
            }
          
        }
    }
}
