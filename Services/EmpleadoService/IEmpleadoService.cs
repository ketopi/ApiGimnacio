using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.EmpleadoService
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoResponseDto>> GetAllEmpleadosAsync();
        Task<EmpleadoResponseDto?> GetEmpleadoByIdAsync(int id);
        Task<EmpleadoResponseDto> CreateEmpleadoAsync(EmpleadoCreateDto crearEmpleadoDto);
        Task<EmpleadoResponseDto?> UpdateEmpleadoAsync(int id, EmpleadoUpdateDto updateEmpleadoDto);
        Task<bool> DeleteEmpleadoAsync(int id);
        Task<bool> ExistsAsync(int id);
    
    
        
    }
}