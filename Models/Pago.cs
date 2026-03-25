using System.ComponentModel.DataAnnotations;
using Backend_Gimnacio.Enums;

namespace Backend_Gimnacio.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        // Relación con Suscripción
        [Required]
        public int SuscripcionId { get; set; }
        public Suscripcion Suscripcion { get; set; } = null!;

        //  Monto pagado
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        //  Fecha de pago
        public DateTime FechaPago { get; set; } = DateTime.UtcNow;

        //  Método de pago (Efectivo, QR, etc.)
        [Required]
        public MetodoPago MetodoPago { get; set; }

        //  Estado 
        public bool Estado { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;        
    }
}