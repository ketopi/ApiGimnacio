using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Enums;
using Backend_Gimnacio.Helpers;
using Backend_Gimnacio.Services.PagoService;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gimnacio.Controllers.V1
{
    /// <summary>
    /// Controlador para la gestión de pagos del gimnasio.
    /// Permite consultar, registrar, actualizar y eliminar pagos.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        private readonly ILogger<PagoController> _logger;

        /// <summary>
        /// Constructor del controlador de pagos.
        /// </summary>
        /// <param name="pagoService">Servicio de lógica de pagos.</param>
        /// <param name="logger">Servicio de logging.</param>
        public PagoController(IPagoService pagoService, ILogger<PagoController> logger)
        {
            _pagoService = pagoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de todos los pagos registrados.
        /// </summary>
        /// <returns>Lista de pagos.</returns>
        /// <response code="200">Retorna la lista de pagos.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<PagoResponseDto>>> GetAllAsync()
        {
            _logger.LogInformation("Consultando todos los pagos");

            var pagos = await _pagoService.GetAllAsync();

            return Ok(pagos);
        }

        /// <summary>
        /// Obtiene un pago por su identificador.
        /// </summary>
        /// <param name="id">ID del pago.</param>
        /// <returns>Pago encontrado.</returns>
        /// <response code="200">Pago encontrado.</response>
        /// <response code="404">Pago no encontrado.</response>
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PagoResponseDto>> GetById(int id)
        {
            _logger.LogInformation("Consultando pago con ID {PagoId}", id);

            var pago = await _pagoService.GetByIdAsync(id);

            if (pago == null)
            {
                _logger.LogWarning("Pago con ID {PagoId} no encontrado", id);
                return NotFound(new { message = $"Pago con ID {id} no encontrado" });
            }

            return Ok(pago);
        }

        /// <summary>
        /// Registra un nuevo pago en el sistema.
        /// </summary>
        /// <param name="dto">Datos del pago a registrar.</param>
        /// <returns>Pago creado.</returns>
        /// <response code="201">Pago creado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PagoResponseDto>> AddAsync([FromBody] PagoCreateDto dto)
        {
            _logger.LogInformation(
                "Registrando pago | SuscripcionId: {SuscripcionId} | Monto: {Monto}",
                dto.SuscripcionId,
                dto.Monto
            );

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido al registrar pago");
                return BadRequest(ModelState);
            }

            var pago = await _pagoService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = pago.Id, version = "1.0" },
                pago
            );
        }

        /// <summary>
        /// Actualiza el estado de un pago.
        /// </summary>
        /// <param name="id">ID del pago.</param>
        /// <param name="dto">Nuevo estado del pago.</param>
        /// <returns>Pago actualizado.</returns>
        /// <response code="200">Estado actualizado correctamente.</response>
        /// <response code="400">Error en la solicitud.</response>
        /// <response code="404">Pago no encontrado.</response>
        [HttpPatch("{id:int}/estado")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PagoResponseDto>> UpdateEstado(int id, [FromBody] PagoUpdateDto dto)
        {
            _logger.LogInformation(
                "Actualizando estado del pago | PagoId: {PagoId} | NuevoEstado: {Estado}",
                id,
                dto.Estado
            );

            try
            {
                var pago = await _pagoService.UpdateEstadoAsync(id, dto);

                if (pago == null)
                {
                    _logger.LogWarning("Pago con ID {PagoId} no encontrado", id);
                    return NotFound(new { message = $"Pago con ID {id} no encontrado" });
                }

                return Ok(pago);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un pago del sistema.
        /// </summary>
        /// <param name="id">ID del pago.</param>
        /// <returns>No retorna contenido.</returns>
        /// <response code="204">Pago eliminado correctamente.</response>
        /// <response code="404">Pago no encontrado.</response>
        [HttpDelete("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Eliminando pago con ID {PagoId}", id);

            var eliminado = await _pagoService.DeleteAsync(id);

            if (!eliminado)
            {
                _logger.LogWarning("Pago con ID {PagoId} no encontrado", id);
                return NotFound(new { message = $"Pago con ID {id} no encontrado" });
            }

            return NoContent();
        }

        /// <summary>
        /// Metodos de Pago
        /// </summary>
        [HttpGet("metodos-pago")]
        public IActionResult GetMetodosPago()
        {
            var lista = Enum.GetValues(typeof(MetodoPago))
                .Cast<MetodoPago>()
                .Select(x => new
                {
                    id = (int)x,
                    nombre = x.ToString(),
                    descripcion = x.GetDescription()
                });

            return Ok(lista);
        }
    }
}