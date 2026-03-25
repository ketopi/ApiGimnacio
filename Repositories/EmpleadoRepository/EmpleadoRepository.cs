using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gimnacio.Repositories.EmpleadoRepository
{
    public class EmpleadoRepository :IEmpleadoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmpleadoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// Obtiene la lista completa de empleados.
        public async Task<IEnumerable<Empleado>> GetEmpleadosAsync()
        {
            return await _dbContext.Empleados
                .AsNoTracking()
                .ToListAsync();
        }

        /// Obtiene un empleado por su ID.
        public async Task<Empleado?> GetEmpleadoByIdAsync(int id)
        {
            return await _dbContext.Empleados
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// Crea un nuevo empleado en la base de datos.
        public async Task<Empleado> CreateEmpleadoAsync(Empleado empleado)
        {
            await _dbContext.Empleados.AddAsync(empleado);
            await _dbContext.SaveChangesAsync();
            return empleado;
        }

        /// Actualiza un empleado existente.
        public async Task<bool> UpdateEmpleadoAsync(Empleado empleado)
        {
            var existe = await _dbContext.Empleados.AnyAsync(e => e.Id == empleado.Id);
            if (!existe)
                return false;

            _dbContext.Empleados.Update(empleado);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// Elimina un empleado por su ID.
        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var empleado = await _dbContext.Empleados.FindAsync(id);
            if (empleado == null)
                return false;

            _dbContext.Empleados.Remove(empleado);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// Verifica si un empleado existe por ID.
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Empleados.AnyAsync(e => e.Id == id);
        }

    }
}