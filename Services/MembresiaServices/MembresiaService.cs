

using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Repositories.MembresiaRepository;

namespace Backend_Gimnacio.Services.MembresiaServices
{
    public class MembresiaService : IMembresiaService
    {
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly ILogger<MembresiaService> _logger;

        public MembresiaService(
            IMembresiaRepository membresiaRepository,
            ILogger<MembresiaService> logger)
        {
            _membresiaRepository = membresiaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MembresiaResponseDto>> GetAllMembresiasAsync()
        {
            var membresias = await _membresiaRepository.GetMembresiasAsync();
            return membresias.ToResponseDtoList();

        }

        public async Task<MembresiaResponseDto?> GetMembresiaByIdAsync(int id)
        {
            var membresia = await _membresiaRepository.GetMembresiaByIdAsync(id);
            return membresia?.ToResponseDto();

        }

        public async Task<MembresiaResponseDto> CreateMembresiaAsync(MembresiaCreateDto crearMembresiaDto)
        {
            var membresia = crearMembresiaDto.ToEntity();
            var createdMembresia = await _membresiaRepository.CreateAsync(membresia);

            _logger.LogInformation("Membresia creada con ID: {MembresiaId}", createdMembresia.Id);

            return createdMembresia.ToResponseDto();
        }





    }
}