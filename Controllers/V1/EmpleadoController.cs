using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Services.EmpleadoService;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoService _empleadoService;

        /// Constructor del controlador de empleados.   
        public EmpleadoController(
            ILogger<EmpleadoController> logger,
            IEmpleadoService empleadoService
            )
        {
            _logger = logger;
            _empleadoService = empleadoService;

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<EmpleadoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmpleadoResponseDto>>> GetEmpleados()
        {
            _logger.LogInformation("Consultando todos los empleados");

            var empleados = await _empleadoService.GetAllEmpleadosAsync();

            return Ok(empleados);
        }    

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<EmpleadoResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoResponseDto>> GetEmpleado(int id)
        {
            _logger.LogInformation("Buscando empleado con ID {Id}", id);

            var empleado = await _empleadoService.GetEmpleadoByIdAsync(id);
            if (empleado == null)
            {
                _logger.LogWarning("Empleado con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Empleado con ID {id} no encontrado" });
            }

            return Ok(empleado);

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(EmpleadoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoResponseDto>> CreateEmpleado(
            [FromBody] EmpleadoCreateDto createEmpleadoDto)
        {
            _logger.LogInformation("Creando nuevo empleado");

            var empleado = await _empleadoService.CreateEmpleadoAsync(createEmpleadoDto);

            return CreatedAtAction(
                nameof(GetEmpleado),
                new { id = empleado.Id },
                empleado);

        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]  // 👈 tenías PermisoResponseDto
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmpleadoResponseDto>> UpdateEmpleado(
         int id, [FromBody] EmpleadoUpdateDto updateEmpleadoDto)
        {
            _logger.LogInformation("Actualizando empleado con ID: {EmpleadoId}", id);
            var empleado = await _empleadoService.UpdateEmpleadoAsync(id, updateEmpleadoDto);
            if (empleado is null) return NotFound();

            return Ok(empleado);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEmpleado(int id)
        {
            _logger.LogInformation("Eliminando empleado con ID: {EmpleadoId}", id);

            var deleted = await _empleadoService.DeleteEmpleadoAsync(id);
            if (!deleted) return NotFound();

            // Devuelve un mensaje simple
            return Ok(new { message = "Empleado eliminado" });
        }

         [HttpGet("{id:int}/exists")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ExistsAsync(int id)
        {
            _logger.LogInformation("Verificando existencia de empleado con ID {Id}", id);

            var exists = await _empleadoService.ExistsAsync(id);

            if (!exists) return NotFound();

            return Ok();
        }


    }


}