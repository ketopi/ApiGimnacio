using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Backend_Gimnacio.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //necesita el "token"
        [ProducesResponseType(StatusCodes.Status403Forbidden)]  //no tiene permiso para la api  "Policy"
        [Authorize(Policy = "prueba.ver")]
        public IActionResult Get()
        {
            _logger.LogInformation("Acceso a Hello");
            return Ok(new { mensaje = "Hello World" });
        }

        [HttpGet("nuevo")]
        [MapToApiVersion("2.0")]
        [Obsolete("Este método está obsoleto, usar GET principal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //necesita el "token"
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //no tiene permiso para la api  "Policy"
        [Authorize]
        public IActionResult GetNuevo()
        {
            _logger.LogWarning("Acceso a endpoint obsoleto");
            return Ok(new { mensaje = "Nuevo" });
        }
    }
}
