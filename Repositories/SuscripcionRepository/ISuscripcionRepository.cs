
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.SuscripcionRepository
{
    public interface ISuscripcionRepository
    {

        Task<List<Suscripcion>> GetAllAsync();
        Task<Suscripcion?> GetByIdAsync(int id);
        Task<List<Suscripcion>> GetActivasByClienteId(int clienteId);
        Task<Suscripcion> AddAsync(Suscripcion suscripcion);    
        Task<Suscripcion> UpdateAsync(Suscripcion suscripcion);

        Task<bool> DeleteAsync(int id);

        
    }
}