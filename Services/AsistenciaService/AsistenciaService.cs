using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Repositories.AsistenciaRepository;
using Microsoft.Extensions.Logging;

namespace Backend_Gimnacio.Services.AsistenciaService
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly ILogger<AsistenciaService> _logger;

        public AsistenciaService(IAsistenciaRepository asistenciaRepository, ILogger<AsistenciaService> logger)
        {
            _asistenciaRepository = asistenciaRepository;
            _logger = logger;
        }

        // Obtener todas las asistencias
        public async Task<IEnumerable<AsistenciaResponseDto>> GetAllAsync()
        {
            var asistencias = await _asistenciaRepository.GetAllAsync();
            return asistencias.ToResponseDtoList();
        }

        // Obtener asistencias de un cliente específico
        public async Task<IEnumerable<AsistenciaResponseDto>> GetByClienteIdAsync(int clienteId)
        {
            var asistencias = await _asistenciaRepository.GetByClienteIdAsync(clienteId);
            return asistencias.ToResponseDtoList();
        }

        // Registrar nueva asistencia
        public async Task<AsistenciaResponseDto> AddAsync(AsistenciaCreateDto dto)
        {
            var asistencia = dto.ToEntity();

            var created = await _asistenciaRepository.AddAsync(asistencia);

            _logger.LogInformation(
                "Asistencia creada con ID {AsistenciaId} para Cliente {ClienteId} registrada por Usuario {UsuarioId}",
                created.Id,
                created.Suscripcion?.ClienteId, // 🔥 CORREGIDO
                created.RegistradoPorId
            );

            return created.ToResponseDto();
        }
    }
}