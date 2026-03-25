
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.EmpleadoRepository
{
    public interface IEmpleadoRepository
    {

        Task<IEnumerable<Empleado>> GetEmpleadosAsync();
        Task<Empleado?> GetEmpleadoByIdAsync(int id);
        Task<Empleado> CreateEmpleadoAsync(Empleado empleado);
        Task<bool> UpdateEmpleadoAsync(Empleado empleado);
        Task<bool> DeleteEmpleadoAsync(int id);
        Task<bool> ExistsAsync(int id);
    
        
    }
}