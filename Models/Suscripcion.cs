using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Models
{
    public class Suscripcion
    {
        [Key]
        public int Id { get; set; }

        // FK a Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // FK a Membresia
        public int MembresiaId { get; set; }
        public Membresia Membresia { get; set; } = null!;

        // Fechas de inicio y fin
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Estado activo o inactivo
        public bool Estado { get; set; } = true;

        // Opcional: fecha de creación automática
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;



        //relacion con asistencia
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();


        //relacion con pagos
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    
    }
}