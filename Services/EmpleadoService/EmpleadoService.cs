

using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Repositories.EmpleadoRepository;

namespace Backend_Gimnacio.Services.EmpleadoService
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        private readonly ILogger<EmpleadoService> _logger;


        public EmpleadoService(IEmpleadoRepository empleadoRepository
        , ILogger<EmpleadoService> logger)
        {
            _empleadoRepository = empleadoRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<EmpleadoResponseDto>> GetAllEmpleadosAsync()
        {
            var empleados = await _empleadoRepository.GetEmpleadosAsync();
            return empleados.ToResponseDtoList();
        }

        public async Task<EmpleadoResponseDto?> GetEmpleadoByIdAsync(int id)
        {
            var empleado = await _empleadoRepository.GetEmpleadoByIdAsync(id);
            return empleado?.ToResponseDto();

        }
        public async Task<EmpleadoResponseDto> CreateEmpleadoAsync(EmpleadoCreateDto crearEmpleadoDto)
        {
            var empleado = crearEmpleadoDto.ToEntity();
            var createdEmpleado = await _empleadoRepository.CreateEmpleadoAsync(empleado);

            _logger.LogInformation("Empleado creado con ID: {EmpleadoId}", createdEmpleado.Id);

            return createdEmpleado.ToResponseDto();
        }

        public async Task<EmpleadoResponseDto?> UpdateEmpleadoAsync(int id, EmpleadoUpdateDto updateEmpleadoDto)
        {
            var empleado = await _empleadoRepository.GetEmpleadoByIdAsync(id);
            if (empleado is null) return null;

            // Mapper 
            empleado.UpdateEntity(updateEmpleadoDto);

            var success = await _empleadoRepository.UpdateEmpleadoAsync(empleado);
            if (!success) return null;

            _logger.LogInformation("Empleado actualizado con ID: {EmpleadoId}", empleado.Id);

            return empleado.ToResponseDto();
        }
        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var empleado = await _empleadoRepository.GetEmpleadoByIdAsync(id);
            if (empleado is null) return false;

            await _empleadoRepository.DeleteEmpleadoAsync(empleado.Id);

            _logger.LogInformation("Empleado eliminado con ID: {EmpleadoId}", id);

            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _empleadoRepository.ExistsAsync(id);
        }

    }




}
