
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.PermisosServices
{
    public interface IPermisosService
    {
        Task<IEnumerable<PermisoResponseDto>> GetAllPermisosAsync();
        Task<PermisoResponseDto?> GetPermisoByIdAsync(int id);
        Task<PermisoResponseDto> CreatePermisoAsync(PermisoCreateDto crearPermisoDto);
        
        
    }
}