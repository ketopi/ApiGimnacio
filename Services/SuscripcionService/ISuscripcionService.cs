
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.SuscripcionService
{
    public interface ISuscripcionService
    {

        Task<IEnumerable<SuscripcionResponseDto>> GetAllAsync();
        Task<SuscripcionResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<SuscripcionResponseDto>> GetAllSuscripcionesAsync(int clienteId);
        Task<SuscripcionResponseDto> AddAsync(SuscripcionCreateDto crearSuscripcionDto);
        Task<SuscripcionResponseDto?> UpdateAsync(int id, SuscripcionUpdateDto updateSuscripcionDto);
        Task<bool> DeleteAsync(int id);
        
        
    }
}