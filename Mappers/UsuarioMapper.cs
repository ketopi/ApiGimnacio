using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Mappers
{
    public static class UsuarioMapper
    {
        // Entity → Response DTO
        public static UsuarioResponseDto ToResponseDto(this Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Estado = usuario.Estado,
                CreatedAt = usuario.CreatedAt,
                UltimoAcceso = usuario.UltimoAcceso,

                // 🔥 datos relacionados
                RolNombre = usuario.Rol?.Nombre ?? string.Empty,
                EmpleadoNombre = usuario.Empleado?.Nombre ?? string.Empty
            };
        }

        // Lista
        public static IEnumerable<UsuarioResponseDto> ToResponseDtoList(this IEnumerable<Usuario> usuarios)
        {
            return usuarios.Select(u => u.ToResponseDto());
        }

        // Create DTO → Entity
        public static Usuario ToEntity(this UsuarioCreateDto dto, string passwordHash)
        {
            return new Usuario
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                EmpleadoId = dto.EmpleadoId,
                RolId = dto.RolId,
                Estado = true,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        // Update
        public static void UpdateEntity(this Usuario usuario, UsuarioUpdateDto dto, string? passwordHash = null)
        {
            // Actualizar username solo si viene un valor válido
            if (!string.IsNullOrWhiteSpace(dto.Username))
                usuario.Username = dto.Username!; // el ! es seguro aquí porque ya verificamos null/empty

            // Actualizar rol solo si viene un valor
            if (dto.RolId.HasValue)
                usuario.RolId = dto.RolId.Value;

            // Actualizar estado solo si viene un valor
            if (dto.Estado.HasValue)
                usuario.Estado = dto.Estado.Value;

            // Actualizar hash de contraseña solo si se pasó uno
            if (!string.IsNullOrEmpty(passwordHash))
                usuario.PasswordHash = passwordHash;
        }
    }
}