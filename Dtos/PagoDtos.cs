
using System.ComponentModel.DataAnnotations;


namespace Backend_Gimnacio.Dtos
{
    public class PagoCreateDto
    {
        [Required]
        public int SuscripcionId { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public string MetodoPago { get; set; } = string.Empty;
    }
    public class PagoResponseDto
    {
        public int Id { get; set; }

        public int SuscripcionId { get; set; }

        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }

        public string MetodoPago { get; set; } = string.Empty;

        public bool Estado { get; set; }
    }
    public class PagoUpdateDto
    {
        [Required]
        public bool Estado { get; set; }
    }
}