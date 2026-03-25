using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Repositories.MembresiaRepository;
using Backend_Gimnacio.Repositories.SuscripcionRepository;

namespace Backend_Gimnacio.Services.SuscripcionService
{
    public class SuscripcionService : ISuscripcionService
    {
        private readonly ISuscripcionRepository _suscripcionRepository;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly ILogger<SuscripcionService> _logger;

        // Constructor: inyecta repositorio y logger
        public SuscripcionService(
            ISuscripcionRepository suscripcionRepository,
            ILogger<SuscripcionService> logger,
            IMembresiaRepository membresiaRepository)
        {
            _suscripcionRepository = suscripcionRepository;
            _logger = logger;
            _membresiaRepository = membresiaRepository;
        }
        // obtner todas las suscripciones
        public async Task<IEnumerable<SuscripcionResponseDto>> GetAllAsync()
        {
            var suscripciones = await _suscripcionRepository.GetAllAsync();

            // Convierte la lista de entidades a DTOs
            return suscripciones.ToResponseDtoList();
        }

        //obtene por id 
        public async Task<SuscripcionResponseDto?> GetByIdAsync(int id)
        {
            var suscripciones = await _suscripcionRepository.GetByIdAsync(id);
            if (suscripciones is null) return null;


            // Convierte la lista de entidades a DTOs   
            return suscripciones.ToResponseDto();
        }

        // Obtener todas las suscripciones ACTIVAS de un cliente
        public async Task<IEnumerable<SuscripcionResponseDto>> GetAllSuscripcionesAsync(int clienteId)
        {
            // Llama al repositorio para obtener datos desde la BD
            var suscripciones = await _suscripcionRepository
                .GetActivasByClienteId(clienteId);

            // Convierte entidades a DTOs para enviar al frontend
            return suscripciones.ToResponseDtoList();
        }

        // Crear suscripciones
        public async Task<SuscripcionResponseDto> AddAsync(SuscripcionCreateDto crearSuscripcionDto)
        {
            // Convertir DTO a entidad
            var suscripcion = crearSuscripcionDto.ToEntity();

            // Fecha de inicio automática
            suscripcion.FechaInicio = DateTime.UtcNow;

            // Traer la membresía para saber la duración
            var membresia = await _membresiaRepository.GetMembresiaByIdAsync(suscripcion.MembresiaId);
            if (membresia == null)
                throw new Exception($"Membresía con ID {suscripcion.MembresiaId} no encontrada.");

            // Calcular fechaFin
            suscripcion.FechaFin = suscripcion.FechaInicio.AddDays(membresia.DuracionDias);

            // Guardar en la BD
            var createdSuscripcion = await _suscripcionRepository.AddAsync(suscripcion);

            // Recargar la entidad con Cliente y Membresía para nombres
            var suscripcionCompleta = await _suscripcionRepository.GetByIdAsync(createdSuscripcion.Id);
            if (suscripcionCompleta == null)
                throw new Exception("No se pudo recuperar la suscripción recién creada.");

            // Logger
            _logger.LogInformation(
                "Suscripción creada con ID {SuscripcionId} para Cliente {ClienteId} y Membresia {MembresiaId}",
                suscripcionCompleta.Id, suscripcionCompleta.ClienteId, suscripcionCompleta.MembresiaId
            );

            // Devolver DTO
            return suscripcionCompleta.ToResponseDto();
        }

        // Editar suscripcion
        public async Task<SuscripcionResponseDto?> UpdateAsync(int id, SuscripcionUpdateDto updateSuscripcionDto)
        {
            // Obtener la suscripción por id
            var suscripcion = await _suscripcionRepository.GetByIdAsync(id);
            if (suscripcion is null) return null;

            // Actualizar la entidad con los datos del DTO usando el mapper
            suscripcion.UpdateEntity(updateSuscripcionDto);

            // Guardar cambios en la base de datos
            var updatedSuscripcion = await _suscripcionRepository.UpdateAsync(suscripcion);

            // Registrar log
            _logger.LogInformation("Suscripcion actualizada con ID: {SuscripcionId}", updatedSuscripcion.Id);

            // 5Devolver DTO de respuesta
            return updatedSuscripcion.ToResponseDto();
        }

        // Eliminar suscripcion
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _suscripcionRepository.DeleteAsync(id);
            _logger.LogInformation("Suscripcion eliminada con ID: {SuscripcionId}", id);
            return deleted;
        }




    }
}