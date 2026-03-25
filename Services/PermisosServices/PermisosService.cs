using Backend_Gimnacio.Repositories.PermisosRepository;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;

namespace Backend_Gimnacio.Services.PermisosServices.PermisosService;

public class PermisosService : IPermisosService
{
    private readonly IPermisosRepository _permisosRepository;
    private readonly ILogger<PermisosService> _logger;

    public PermisosService(
        IPermisosRepository permisosRepository,
        ILogger<PermisosService> logger)
    {
        _permisosRepository = permisosRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PermisoResponseDto>> GetAllPermisosAsync()
    {
        var permisos = await _permisosRepository.GetPermisosAsync();
        return permisos.ToResponseDtoList();
    }

    public async Task<PermisoResponseDto?> GetPermisoByIdAsync(int id)
    {
        var permiso = await _permisosRepository.GetPermisoByIdAsync(id);
        return permiso?.ToResponseDto();
    }

    public async Task<PermisoResponseDto> CreatePermisoAsync(PermisoCreateDto crearPermisoDto)
    {
        var permiso = crearPermisoDto.ToEntity();
        var createdPermiso = await _permisosRepository.CreateAsync(permiso);

        _logger.LogInformation("Permiso creado con ID: {PermisoId}", createdPermiso.Id);

        return createdPermiso.ToResponseDto();
    }
}