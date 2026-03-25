using Backend_Gimnacio.Dtos;   
using Backend_Gimnacio.Models; 

namespace Backend_Gimnacio.Mappers
{
    // Clase estática que contiene métodos para mapear entre entidades y DTOs
    public static class PermisoMapper
    {
        // Método de extensión que convierte una entidad Permisos a un DTO de respuesta
        public static PermisoResponseDto ToResponseDto(this Permisos permiso)
        {
            return new PermisoResponseDto
            {
                Id = permiso.Id,                   // Copia el Id
                Nombre = permiso.Nombre,           // Copia el Nombre
                Descripcion = permiso.Descripcion  // Copia la Descripción
            };
        }

        // Método de extensión que convierte una lista de entidades a lista de DTOs
        public static IEnumerable<PermisoResponseDto> ToResponseDtoList(this IEnumerable<Permisos> permisos)
        {
            // Usa LINQ para recorrer cada permiso y convertirlo a DTO
            return permisos.Select(p => p.ToResponseDto());
        }

        // Método de extensión que convierte un DTO de creación en una entidad Permisos
        public static Permisos ToEntity(this PermisoCreateDto dto)
        {
            return new Permisos
            {
                Nombre = dto.Nombre,           // Asigna el nombre desde el DTO
                Descripcion = dto.Descripcion  // Asigna la descripción desde el DTO
            };
        }

        // Método de extensión que actualiza una entidad existente con datos de un DTO
        public static void UpdateEntity(this Permisos permiso, PermisoUpdateDto dto)
        {
            // Solo actualiza el Nombre si viene con valor (no es null)
            if (dto.Nombre != null) 
                permiso.Nombre = dto.Nombre;

            // Solo actualiza la Descripción si viene con valor (no es null)
            if (dto.Descripcion != null) 
                permiso.Descripcion = dto.Descripcion;
        }
    }
}