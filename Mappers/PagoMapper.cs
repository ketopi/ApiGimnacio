using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Enums;

namespace Backend_Gimnacio.Mappers
{
    public static class PagoMapper
    {
        // =========================
        // DTO → ENTITY
        // =========================
        public static Pago ToEntity(this PagoCreateDto dto)
        {
            return new Pago
            {
                SuscripcionId = dto.SuscripcionId,
                Monto = dto.Monto,

                // 🔥 Conversión segura de string a enum
                MetodoPago = Enum.TryParse<MetodoPago>(dto.MetodoPago, true, out var metodo)
                    ? metodo
                    : MetodoPago.Efectivo, // valor por defecto

                FechaPago = DateTime.UtcNow,
                Estado = true
            };
        }

        // =========================
        // ENTITY → DTO
        // =========================
        public static PagoResponseDto ToResponseDto(this Pago pago)
        {
            return new PagoResponseDto
            {
                Id = pago.Id,

                SuscripcionId = pago.SuscripcionId,

                // 🔥 Cliente desde Suscripción (relación)
                ClienteId = pago.Suscripcion?.ClienteId ?? 0,

                ClienteNombre = pago.Suscripcion?.Cliente != null
                    ? $"{pago.Suscripcion.Cliente.Nombre} {pago.Suscripcion.Cliente.AP}"
                    : "Sin cliente",

                Monto = pago.Monto,

                FechaPago = pago.FechaPago,

                // 🔥 Enum → string
                MetodoPago = pago.MetodoPago.ToString(),

                Estado = pago.Estado
            };
        }

        // =========================
        // LISTA → DTO LISTA
        // =========================
        public static IEnumerable<PagoResponseDto> ToResponseDtoList(this IEnumerable<Pago> pagos)
        {
            return pagos.Select(p => p.ToResponseDto());
        }
    }
}