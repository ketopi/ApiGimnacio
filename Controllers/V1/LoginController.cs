
using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;


namespace Backend_Gimnacio.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginService _loginService;
        private readonly JwtSettings _jwtSettings;

        public LoginController(
            ILogger<LoginController> logger,
            LoginService loginService,
            IOptions<JwtSettings> jwtOptions)
        {
            _logger = logger;
            _loginService = loginService;
            _jwtSettings = jwtOptions.Value;
        }

        /// <summary>
        /// Iniciar sesión con usuario y contraseña.
        /// </summary>
        /// <param name="loginDto">DTO con username y password</param>
        /// <returns>Usuario con token JWT válido hasta las 22:00</returns>
        [HttpPost("login")]
        [EnableRateLimiting("login")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.LoginAsync(loginDto, _jwtSettings);

            if (result == null)
            {
                _logger.LogWarning("Intento de login fallido para el usuario {Username}", loginDto.Username);
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }

            _logger.LogInformation("Usuario {Username} inició sesión correctamente", result.Username);
            return Ok(result);
        }
    }
}