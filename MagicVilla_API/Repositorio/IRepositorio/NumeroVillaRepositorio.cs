﻿using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }
        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
           entidad.FechaActualizacion=DateTime.Now; 
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad; 
        }
    }
}
