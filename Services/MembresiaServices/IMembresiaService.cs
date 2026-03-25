
using Backend_Gimnacio.Repositories.MembresiaRepository;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;

namespace Backend_Gimnacio.Services.MembresiaServices
{
    public interface IMembresiaService
    {
        Task<IEnumerable<MembresiaResponseDto>> GetAllMembresiasAsync();
        Task<MembresiaResponseDto?> GetMembresiaByIdAsync(int id);
        Task<MembresiaResponseDto> CreateMembresiaAsync(MembresiaCreateDto crearMembresiaDto);



    }
}