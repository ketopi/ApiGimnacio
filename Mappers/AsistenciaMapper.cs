using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Mappers
{
    public static class AsistenciaMapper
    {
        // Entity → Response DTO
        public static AsistenciaResponseDto ToResponseDto(this Asistencia asistencia)
        {
            return new AsistenciaResponseDto
            {
                Id = asistencia.Id,

                // Cliente desde Suscripción
                ClienteId = asistencia.Suscripcion?.ClienteId ?? 0,
                ClienteNombre = asistencia.Suscripcion?.Cliente?.Nombre ?? "",

                SuscripcionId = asistencia.SuscripcionId,
                NombreMembresia = asistencia.Suscripcion?.Membresia?.Nombre ?? "",

                // 🔥 CORREGIDO
                RegistradoPorId = asistencia.RegistradoPorId,

                NombreRegistrador = asistencia.RegistradoPor?.Empleado != null
                    ? $"{asistencia.RegistradoPor.Empleado.Nombre} {asistencia.RegistradoPor.Empleado.AP} {asistencia.RegistradoPor.Empleado.AM}"
                    : "",

                FechaHoraIngreso = asistencia.FechaHoraIngreso,

                // ⚠️ Solo si luego agregas esto al modelo
                FechaHoraSalida = null
            };
        }

        // Lista
        public static IEnumerable<AsistenciaResponseDto> ToResponseDtoList(this IEnumerable<Asistencia> asistencias)
        {
            return asistencias.Select(a => a.ToResponseDto());
        }

        // DTO → Entity
        public static Asistencia ToEntity(this AsistenciaCreateDto dto)
        {
            return new Asistencia
            {
                SuscripcionId = dto.SuscripcionId,

                // 🔥 CORREGIDO
                RegistradoPorId = dto.RegistradoPorId,

                FechaHoraIngreso = DateTime.UtcNow
            };
        }
    }
}