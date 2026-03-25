
using Asp.Versioning;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Services.ClienteServices;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Gimnacio.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;

        /// Constructor del controlador de clientes.
        public ClienteController(
            ILogger<ClienteController> logger,
            IClienteService clienteService
            )
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetClientes()
        {
            _logger.LogInformation("Consultando todos los clientes");

            var clientes = await _clienteService.GetAllClientesAsync();

            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente(int id)
        {
            _logger.LogInformation("Buscando cliente con ID {Id}", id);
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                _logger.LogWarning("Cliente con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
            }
            return Ok(cliente);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteResponseDto>> CreateCliente(
            [FromBody] ClienteCreateDto createClienteDto)
        {
            _logger.LogInformation("Creando nuevo cliente ");
            var cliente = await _clienteService.CreateClienteAsync(createClienteDto);
            return CreatedAtAction(
                nameof(GetCliente),
                new { id = cliente.Id },
                cliente);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]  // 👈 tenías PermisoResponseDto
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteResponseDto>> UpdateCliente(
         int id, ClienteUpdateDto updateClienteDto)
        {
            _logger.LogInformation("Actualizando cliente con ID: {ClienteId}", id);

            var cliente = await _clienteService.UpdateClienteAsync(id, updateClienteDto);
            if (cliente is null) return NotFound();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            _logger.LogInformation("Eliminando cliente con ID: {ClienteId}", id);

            var deleted = await _clienteService.DeleteClienteAsync(id);
            if (!deleted) return NotFound();

            // Devuelve un mensaje simple
            return Ok(new { message = "Cliente eliminado" });
        }
    }
}