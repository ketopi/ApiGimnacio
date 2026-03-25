using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Enums;
using Backend_Gimnacio.Services.MembresiaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MembresiaController : ControllerBase
    {
        private readonly ILogger<MembresiaController> _logger;
        private readonly IMembresiaService _membresiaService;

        /// Constructor del controlador de permisos.
        public MembresiaController(
            ILogger<MembresiaController> logger,
            IMembresiaService membresiaService)
        {
            _logger = logger;
            _membresiaService = membresiaService;

        }

        /// <summary>
        /// Obtiene todas los membresias registradas en el sistema.
        /// </summary>       
        /// <response code="200">Lista obtenida correctamente.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<MembresiaResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MembresiaResponseDto>>> GetMembresias()
        {
            _logger.LogInformation("Consultando todas las membresias");

            var membresias = await _membresiaService.GetAllMembresiasAsync();

            return Ok(membresias);

        }


        /// <summary>
        /// Obtiene una menbresia específica mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la membresia.</param>      
        /// <response code="200">Permiso encontrado.</response>
        /// <response code="404">Permiso no encontrado.</response>
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(PermisoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MembresiaResponseDto>> GetMembresia(int id)
        {
            _logger.LogInformation("Buscando membresia con ID {Id}", id);

            var membresia = await _membresiaService.GetMembresiaByIdAsync(id);

            if (membresia == null)
            {
                _logger.LogWarning("Membresia con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Membresia con ID {id} no encontrado" });
            }

            return Ok(membresia);
        }

        /// <summary>
        /// Registra una nueva membresia en el sistema.
        /// </summary>     
        /// <param name="createMembresiaDto">Datos necesarios para crear la membresia.</param>       
        /// <response code="201">Membresia creada correctamente.</response>
        /// <response code="400">Solicitud inválida.</response>

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(PermisoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MembresiaResponseDto>> CreateMembresia(
            [FromBody] MembresiaCreateDto createMembresiaDto)
        {
            _logger.LogInformation("Creando nueva membresia");

            var membresia = await _membresiaService.CreateMembresiaAsync(createMembresiaDto);

            return CreatedAtAction(
                nameof(GetMembresia),
                new { id = membresia.Id },
                membresia);
        }


        [HttpGet("TiposPlan")]
        [MapToApiVersion("1.0")]       
        public IActionResult GetTiposPlan()
        {
            var tiposPlan = Enum.GetValues<TipoPlan>()
                .Select(t => new
                {
                    valor = (int)t,
                    nombre = t.ToString(),
                    diasDuracion = t switch
                    {
                        TipoPlan.Semanal => 7,
                        TipoPlan.Quincenal => 15,
                        TipoPlan.Mensual => 30,
                        TipoPlan.Trimestral => 90,
                        TipoPlan.Anual => 365,
                        _ => 0
                    }
                });

            return Ok(tiposPlan);
        }



    }
}