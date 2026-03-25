using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.PagoService
{
    public interface IPagoService
    {

        Task<IEnumerable<PagoResponseDto>> GetAllAsync();
        Task<PagoResponseDto?> GetByIdAsync(int id);
        Task<PagoResponseDto> AddAsync(PagoCreateDto dto);
        Task<PagoResponseDto?> UpdateEstadoAsync(int id, PagoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        
    }
}