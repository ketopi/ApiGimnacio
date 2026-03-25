using Microsoft.EntityFrameworkCore;
using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.SuscripcionRepository
{
    public class SuscripcionRepository : ISuscripcionRepository
    {
        private readonly ApplicationDbContext _context;

        public SuscripcionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Trae todas las suscripciones de la base de datos
        public async Task<List<Suscripcion>> GetAllAsync()
        {
            return await _context.Suscripciones
                .Include(s => s.Cliente)
                .Include(s => s.Membresia)
                .ToListAsync();
        }

        //Trae solo las suscripciones ACTIVAS de un cliente específico
        public async Task<List<Suscripcion>> GetActivasByClienteId(int clienteId)
        {
            return await _context.Suscripciones
                .Include(s => s.Cliente)
                .Include(s => s.Membresia)
                .Where(s => s.ClienteId == clienteId && s.Estado)
                .ToListAsync();
        }

        //metodo para traer por id 
        public async Task<Suscripcion?> GetByIdAsync(int id)
        {
            return await _context.Suscripciones
                .Include(s => s.Cliente)
                .Include(s => s.Membresia)
                .FirstOrDefaultAsync(s => s.Id == id); 
        }

        //Inserta una nueva suscripción en la base de datos
        public async Task<Suscripcion> AddAsync(Suscripcion suscripcion)
        {
            _context.Suscripciones.Add(suscripcion);
            await _context.SaveChangesAsync();
            return suscripcion;
        }

        //metod para editar 
        public async Task<Suscripcion> UpdateAsync(Suscripcion suscripcion)
        {
            _context.Suscripciones.Update(suscripcion);
            await _context.SaveChangesAsync();
            return suscripcion;
        }

        //metod pra eliminar (logico)
        public async Task<bool> DeleteAsync(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);

            if (suscripcion == null)
                return false;

            suscripcion.Estado = false;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}