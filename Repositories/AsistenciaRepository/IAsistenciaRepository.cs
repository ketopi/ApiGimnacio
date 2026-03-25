using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.AsistenciaRepository
{
    public interface IAsistenciaRepository
    {
        // Obtener todas las asistencias con cliente, suscripción, membresía y registrador completo
        Task<List<Asistencia>> GetAllAsync();
        
        // Obtener asistencias de un cliente específico
        Task<List<Asistencia>> GetByClienteIdAsync(int clienteId);

        // Agregar nueva asistencia
        Task<Asistencia> AddAsync(Asistencia asistencia);


    }
}