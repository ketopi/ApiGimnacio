

using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Models
{
    public class Asistencia
    {

        [Key]
        public int Id { get; set; }

        // Suscripción asociada
        public int SuscripcionId { get; set; }
        public Suscripcion Suscripcion { get; set; } = null!;

        // Usuario que registró la asistencia (puede ser un empleado/admin)
        public int RegistradoPorId { get; set; }
        public Usuario RegistradoPor { get; set; } = null!;

        // Fecha y hora de ingreso
        public DateTime FechaHoraIngreso { get; set; } = DateTime.UtcNow;


       
    

    }
}