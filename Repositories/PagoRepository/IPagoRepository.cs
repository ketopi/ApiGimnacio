using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.PagoRepository
{
    public interface IPagoRepository
    {

        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task<Pago> AddAsync(Pago pago);
        Task<Pago?> UpdateAsync(Pago pago);
        Task<bool> DeleteAsync(int id);
    
        
    }
}