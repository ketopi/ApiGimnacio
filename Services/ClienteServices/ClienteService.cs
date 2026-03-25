using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Repositories.ClienteRepository;

namespace Backend_Gimnacio.Services.ClienteServices
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(
            IClienteRepository clienteRepository,
            ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ClienteResponseDto>> GetAllClientesAsync()
        {
            var clientes = await _clienteRepository.GetClientesAsync();
            return clientes.ToResponseDtoList();
        }

        public async Task<ClienteResponseDto?> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            return cliente?.ToResponseDto();
        }

        public async Task<ClienteResponseDto> CreateClienteAsync(ClienteCreateDto crearClienteDto)
        {
            var cliente = crearClienteDto.ToModel();
            var createdCliente = await _clienteRepository.CreateAsync(cliente);

            _logger.LogInformation("Cliente creado con ID: {ClienteId}", createdCliente.Id);

            return createdCliente.ToResponseDto();
        }

        public async Task<ClienteResponseDto?> UpdateClienteAsync(int id, ClienteUpdateDto actualizarClienteDto)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente is null) return null;

            actualizarClienteDto.UpdateModel(cliente);
            var updatedCliente = await _clienteRepository.UpdateAsync(cliente);

            _logger.LogInformation("Cliente actualizado con ID: {ClienteId}", updatedCliente.Id);

            return updatedCliente.ToResponseDto();
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente is null) return false;

            await _clienteRepository.DeleteAsync(cliente);

            _logger.LogInformation("Cliente eliminado con ID: {ClienteId}", id);

            return true;
        }
    }
}