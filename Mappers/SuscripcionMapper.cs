using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Mappers
{
    public static class SuscripcionMapper
    {
        // Entity → Response DTO
        public static SuscripcionResponseDto ToResponseDto(this Suscripcion suscripcion)
        {
            return new SuscripcionResponseDto
            {
                Id = suscripcion.Id,
                ClienteId = suscripcion.ClienteId,
                ClienteNombre = suscripcion.Cliente?.Nombre ?? "",

                MembresiaId = suscripcion.MembresiaId,
                MembresiaNombre = suscripcion.Membresia?.Nombre ?? "",

                FechaInicio = suscripcion.FechaInicio,
                FechaFin = suscripcion.FechaFin,
                Estado = suscripcion.Estado,
                CreatedAt = suscripcion.CreatedAt,
            };
        }

        // Lista
        public static IEnumerable<SuscripcionResponseDto> ToResponseDtoList(this IEnumerable<Suscripcion> suscripciones)
        {
            return suscripciones.Select(s => s.ToResponseDto());
        }

        // Create DTO → Entity
        // Create DTO → Entity
        public static Suscripcion ToEntity(this SuscripcionCreateDto dto)
        {
            return new Suscripcion
            {
                ClienteId = dto.ClienteId,
                MembresiaId = dto.MembresiaId,
                Estado = true
            };
        }

        // Update (modifica el objeto existente)
        public static void UpdateEntity(this Suscripcion suscripcion, SuscripcionUpdateDto dto)
        {
            suscripcion.FechaInicio = dto.FechaInicio;
            suscripcion.FechaFin = dto.FechaFin;
            suscripcion.Estado = dto.Estado;
        }
    }
}