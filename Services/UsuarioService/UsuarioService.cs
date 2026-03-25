
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Repositories.UsuarioRepository;

namespace Backend_Gimnacio.Services.UsuarioService
{
    public class UsuarioService :IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.ToResponseDtoList();
        }

        public async Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario?.ToResponseDto();
        }

        public async Task<UsuarioResponseDto> CreateUsuarioAsync(UsuarioCreateDto crearUsuarioDto)
        {
            // Generar hash seguro
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(crearUsuarioDto.Password);

            // Mapear DTO -> Entity
            var usuario = crearUsuarioDto.ToEntity(passwordHash);

            // Guardar en DB
            var createdUsuario = await _usuarioRepository.CreateAsync(usuario);

            // Loggear creación
            _logger.LogInformation("Usuario creado con ID: {UsuarioId}", createdUsuario.Id);

            // Retornar DTO
            return createdUsuario.ToResponseDto();
        }

        public async Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateDto updateUsuarioDto, string? hashedPassword = null)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null) return null;

            // Mapper
            existingUsuario.UpdateEntity(updateUsuarioDto, hashedPassword);

            // Actualizar
            var updatedUsuario = await _usuarioRepository.UpdateAsync(existingUsuario);
            if (updatedUsuario == null) return null;

            _logger.LogInformation("Usuario actualizado con ID: {UsuarioId}", updatedUsuario.Id);

            return updatedUsuario.ToResponseDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _usuarioRepository.DeleteAsync(id);
            if (deleted)
                _logger.LogInformation("Usuario eliminado con ID: {UsuarioId}", id);
            return deleted; 
        }

    }
}