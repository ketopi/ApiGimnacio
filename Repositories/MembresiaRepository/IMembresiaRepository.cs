using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Repositories.MembresiaRepository
{
    public interface IMembresiaRepository
    {
        Task<IEnumerable<Membresia>> GetMembresiasAsync();
        Task<Membresia?> GetMembresiaByIdAsync(int id);
        Task<Membresia> CreateAsync(Membresia membresia);
        
        
    }
}