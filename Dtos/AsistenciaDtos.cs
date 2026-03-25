
using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Dtos
{
    public class AsistenciaCreateDto
    {
        [Required(ErrorMessage = "La suscripción es requerida")]
        public int SuscripcionId { get; set; }

        [Required(ErrorMessage = "El empleado es requerido")]
        public int RegistradoPorId { get; set; }
    }

    public class AsistenciaResponseDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;

        public int SuscripcionId { get; set; }
        public string NombreMembresia { get; set; } = string.Empty;

        public int RegistradoPorId { get; set; } // 🔥 CORREGIDO
        public string NombreRegistrador { get; set; } = string.Empty;

        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}