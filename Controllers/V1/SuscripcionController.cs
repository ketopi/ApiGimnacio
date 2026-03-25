using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Services.SuscripcionService;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SuscripcionController : Controller
    {
        private readonly ILogger<SuscripcionController> _logger;
        private readonly ISuscripcionService _suscripcionService;



        /// Constructor del controlador de permisos.
        public SuscripcionController(ILogger<SuscripcionController> logger, ISuscripcionService suscripcionService)
        {
            _logger = logger;
            _suscripcionService = suscripcionService;

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<SuscripcionResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SuscripcionResponseDto>>> GetSuscripciones()
        {
            _logger.LogInformation("Consultando todas las suscripciones");

            var suscripciones = await _suscripcionService.GetAllAsync();

            return Ok(suscripciones);
        }
        /// <summary>
        ///  buscar por id alguna suscripcion
        /// </summary>    
        /// 
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SuscripcionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuscripcionResponseDto>> GetById(int id)
        {
            var suscripcion = await _suscripcionService.GetByIdAsync(id);
            if (suscripcion is null) return NotFound();
            return Ok(suscripcion);
        }

        /// <summary>
        ///  Se debe pasr el id del cliente para que vote todos los suscripciones activas
        /// </summary>    
        [HttpGet("cliente/{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<SuscripcionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SuscripcionResponseDto>>> GetAllSuscripcionesAsync(int id)
        {
            _logger.LogInformation("Buscando suscripciones del cliente con ID {Id}", id);

            var suscripciones = await _suscripcionService.GetAllSuscripcionesAsync(id);

            if (suscripciones == null || !suscripciones.Any())
            {
                _logger.LogWarning("No se encontraron suscripciones para el cliente con ID {Id}", id);
                return NotFound(new { message = $"No se encontraron suscripciones para el cliente con ID {id}" });
            }

            return Ok(suscripciones);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SuscripcionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SuscripcionResponseDto>> Create([FromBody] SuscripcionCreateDto dto)
        {
            var suscripcion = await _suscripcionService.AddAsync(dto);
            return CreatedAtAction(
                nameof(GetById),           // debe existir este método GET
                new { id = suscripcion.Id },
                suscripcion);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SuscripcionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SuscripcionResponseDto>> UpdateAsync(
            int id, [FromBody] SuscripcionUpdateDto updateSuscripcionDto)
        {
            _logger.LogInformation("Actualizando suscripcion con ID: {SuscripcionId}", id);

            var suscripcion = await _suscripcionService.UpdateAsync(id, updateSuscripcionDto);
            if (suscripcion is null) return NotFound();

            return Ok(suscripcion);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation("Eliminando suscripcion con ID: {SuscripcionId}", id);

            var deleted = await _suscripcionService.DeleteAsync(id);
            if (!deleted) return NotFound();

            // Devuelve un mensaje simple
            return Ok(new { message = "Suscripcion  eliminada" });
        }



















    }
}