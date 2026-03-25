

using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.AsistenciaService
{
    public interface IAsistenciaService
    {

        // Obtener todas las asistencias
        Task<IEnumerable<AsistenciaResponseDto>> GetAllAsync();

        // Obtener asistencias de un cliente específico
        Task<IEnumerable<AsistenciaResponseDto>> GetByClienteIdAsync(int clienteId);

        // Registrar nueva asistencia
        Task<AsistenciaResponseDto> AddAsync(AsistenciaCreateDto crearAsistenciaDto);


    }
}