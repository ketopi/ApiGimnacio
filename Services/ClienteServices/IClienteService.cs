using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.ClienteServices
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDto>> GetAllClientesAsync();
        Task<ClienteResponseDto?> GetClienteByIdAsync(int id);
        Task<ClienteResponseDto> CreateClienteAsync(ClienteCreateDto crearClienteDto);
        Task<ClienteResponseDto?> UpdateClienteAsync(int id, ClienteUpdateDto actualizarClienteDto);
        Task<bool> DeleteClienteAsync(int id);
        
        
    }
}