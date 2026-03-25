using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Services.PermisosServices;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers.V1
{

    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly ILogger<PermisosController> _logger;
        private readonly IPermisosService _permisosService;


        /// <summary>
        /// Constructor del controlador de permisos.
        /// </summary>
        public PermisosController(
            ILogger<PermisosController> logger,
            IPermisosService permisosService)
        {
            _logger = logger;
            _permisosService = permisosService;
        }


        /// <summary>
        /// Obtiene todos los permisos registrados en el sistema.
        /// </summary>       
        /// <response code="200">Lista obtenida correctamente.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<PermisoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PermisoResponseDto>>> GetPermisos()
        {
            _logger.LogInformation("Consultando todos los permisos");

            var permisos = await _permisosService.GetAllPermisosAsync();

            return Ok(permisos);
        }

        /// <summary>
        /// Registra un nuevo permiso en el sistema.
        /// </summary>     
        /// <param name="createPermisoDto">Datos necesarios para crear el permiso.</param>       
        /// <response code="201">Permiso creado correctamente.</response>
        /// <response code="400">Solicitud inválida.</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(PermisoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PermisoResponseDto>> CreatePermiso(
            [FromBody] PermisoCreateDto createPermisoDto)
        {
            _logger.LogInformation("Creando nuevo permiso");

            var permiso = await _permisosService.CreatePermisoAsync(createPermisoDto);

            return CreatedAtAction(
                nameof(GetPermiso),
                new { id = permiso.Id },
                permiso);
        }


        /// <summary>
        /// Obtiene un permiso específico mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del permiso.</param>      
        /// <response code="200">Permiso encontrado.</response>
        /// <response code="404">Permiso no encontrado.</response>
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(PermisoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PermisoResponseDto>> GetPermiso(int id)
        {
            _logger.LogInformation("Buscando permiso con ID {Id}", id);

            var permiso = await _permisosService.GetPermisoByIdAsync(id);

            if (permiso == null)
            {
                _logger.LogWarning("Permiso con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Permiso con ID {id} no encontrado" });
            }

            return Ok(permiso);
        }


    }
}