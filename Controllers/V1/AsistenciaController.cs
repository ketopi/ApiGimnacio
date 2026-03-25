using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Services.AsistenciaService;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gimnacio.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly ILogger<AsistenciaController> _logger;
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciaController(
            ILogger<AsistenciaController> logger,
            IAsistenciaService asistenciaService)
        {
            _logger = logger;
            _asistenciaService = asistenciaService;
        }

        // 1️⃣ Obtener todas las asistencias
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<AsistenciaResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AsistenciaResponseDto>>> GetAllAsync()
        {
            _logger.LogInformation("Consultando todas las asistencias");

            var asistencias = await _asistenciaService.GetAllAsync();

            return Ok(asistencias);
        }

        // 2️⃣ Obtener asistencia por ID 
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(AsistenciaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AsistenciaResponseDto>> GetById(int id)
        {
            _logger.LogInformation("Consultando asistencia con ID {Id}", id);

            var asistencias = await _asistenciaService.GetAllAsync();
            var asistencia = asistencias.FirstOrDefault(a => a.Id == id);

            if (asistencia == null)
            {
                _logger.LogWarning("Asistencia con ID {Id} no encontrada", id);
                return NotFound(new { message = $"Asistencia con ID {id} no encontrada" });
            }

            return Ok(asistencia);
        }

        // 3️⃣ Obtener asistencias por cliente
        [HttpGet("cliente/{clienteId:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<AsistenciaResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AsistenciaResponseDto>>> GetByClienteIdAsync(int clienteId)
        {
            _logger.LogInformation("Consultando asistencias del cliente con ID {ClienteId}", clienteId);

            var asistencias = await _asistenciaService.GetByClienteIdAsync(clienteId);

            if (!asistencias.Any())
            {
                _logger.LogWarning("No se encontraron asistencias para el cliente {ClienteId}", clienteId);
                return NotFound(new { message = $"No se encontraron asistencias para el cliente {clienteId}" });
            }

            return Ok(asistencias);
        }

   

        /// <summary>
        /// Registra una nueva asistencia en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite registrar el ingreso de un cliente al gimnasio
        /// mediante una suscripción válida y el usuario que realiza el registro.
        /// </remarks>
        /// <param name="dto">
        /// Objeto que contiene:
        /// - SuscripcionId: ID de la suscripción del cliente.
        /// - RegistradoPorId: ID del usuario que registra la asistencia.
        /// </param>
        /// <returns>Asistencia creada correctamente.</returns>
        /// <response code="201">Asistencia registrada exitosamente.</response>
        /// <response code="400">Datos inválidos en la solicitud.</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(AsistenciaResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AsistenciaResponseDto>> AddAsync([FromBody] AsistenciaCreateDto dto)
        {
            _logger.LogInformation(
                "Registrando nueva asistencia | SuscripcionId: {SuscripcionId} | UsuarioId: {UsuarioId}",
                dto.SuscripcionId,
                dto.RegistradoPorId
            );

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido al registrar asistencia");
                return BadRequest(ModelState);
            }

            var asistencia = await _asistenciaService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = asistencia.Id, version = "1.0" }, // 🔥 CLAVE
                asistencia
            );
        }
    }
}