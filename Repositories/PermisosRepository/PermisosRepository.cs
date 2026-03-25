using Microsoft.EntityFrameworkCore;
using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.PermisosRepository
{
    public class PermisosRepository : IPermisosRepository
    {
        private readonly ApplicationDbContext _context;

        public PermisosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //metodo para obtener
        public async Task<IEnumerable<Permisos>> GetPermisosAsync()
        {
            return await _context.Permisos.
            AsNoTracking()
            .ToListAsync();
        }

        //metodo para obtener por id
        public async Task<Permisos?> GetPermisoByIdAsync(int id)
        {
            return await _context.Permisos.
            AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //metodo para crear
        public async Task<Permisos> CreateAsync(Permisos permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }


    }
}