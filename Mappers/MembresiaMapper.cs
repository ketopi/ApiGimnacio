using Backend_Gimnacio.Models;
using Backend_Gimnacio.Dtos;


namespace Backend_Gimnacio.Mappers
{
    public static class MembresiaMapper
    {
        // ENTITY -> RESPONSE DTO
        public static MembresiaResponseDto ToResponseDto(this Membresia membresia)
        {
            return new MembresiaResponseDto
            {
                Id = membresia.Id,
                Nombre = membresia.Nombre,
                Descripcion = membresia.Descripcion,
                DuracionDias = membresia.DuracionDias,
                TipoPlan = membresia.TipoPlan,
                Precio = membresia.Precio,
                Estado = membresia.Estado
            };
        }

        // LIST ENTITY -> LIST DTO
        public static IEnumerable<MembresiaResponseDto> ToResponseDtoList(this IEnumerable<Membresia> membresias)
        {
            return membresias.Select(m => m.ToResponseDto());
        }

        // CREATE DTO -> ENTITY
        public static Membresia ToEntity(this MembresiaCreateDto dto)
        {
            return new Membresia
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                DuracionDias = dto.DuracionDias,
                TipoPlan = dto.TipoPlan,
                Precio = dto.Precio,
                Estado = true // por defecto activo
            };
        }

        // UPDATE DTO -> ENTITY (actualización parcial)
        public static void UpdateEntity(this Membresia membresia, MembresiaUpdateDto dto)
        {
            if (dto.Nombre != null)
                membresia.Nombre = dto.Nombre;

            if (dto.Descripcion != null)
                membresia.Descripcion = dto.Descripcion;

            if (dto.DuracionDias.HasValue)
                membresia.DuracionDias = dto.DuracionDias.Value;

            if (dto.TipoPlan.HasValue)
                membresia.TipoPlan = dto.TipoPlan.Value;

            if (dto.Precio.HasValue)
                membresia.Precio = dto.Precio.Value;

            if (dto.Estado.HasValue)
                membresia.Estado = dto.Estado.Value;
        }
    }
}