
using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        /// Constructor del controlador de usuarios.    
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
        {
            _logger.LogInformation("Consultando todos los usuarios");

            var usuarios = await _usuarioService.GetAllUsuariosAsync();

            return Ok(usuarios);

        }
        
        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuario(int id)
        {
            _logger.LogInformation("Buscando usuario con ID {Id}", id);

            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                _logger.LogWarning("Usuario con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
            }

            return Ok(usuario);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponseDto>> CreateUsuario(
            [FromBody] UsuarioCreateDto createUsuarioDto)
        {
            _logger.LogInformation("Creando nuevo usuario");

            var usuario = await _usuarioService.CreateUsuarioAsync(createUsuarioDto);

            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuario.Id },
                usuario);

        }


        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponseDto>> UpdateUsuario(
            int id, [FromBody] UsuarioUpdateDto updateUsuarioDto)
        {
            _logger.LogInformation("Actualizando usuario con ID: {UsuarioId}", id);

            var usuario = await _usuarioService.UpdateAsync(id, updateUsuarioDto);
            if (usuario is null) return NotFound();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUsuario(int id)        
        {
            _logger.LogInformation("Eliminando usuario con ID: {UsuarioId}", id);

            var deleted = await _usuarioService.DeleteAsync(id);
            if (!deleted) return NotFound();

            // Devuelve un mensaje simple
            return Ok(new { message = "Usuario eliminado" });

        }

    }
}