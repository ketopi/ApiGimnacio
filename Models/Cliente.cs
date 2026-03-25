using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido paterno no puede exceder 100 caracteres.")]
        public string AP { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "El apellido materno no puede exceder 100 caracteres.")]
        public string AM { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CI es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El CI no puede exceder 20 caracteres.")]
        public string CI { get; set; } = string.Empty;

        [Required(ErrorMessage = "El celular es obligatorio.")]
        [Phone(ErrorMessage = "El número de celular no es válido")]
        public int Celular { get; set; }

        public bool Estado { get; set; } = true;


        public DateTimeOffset CreatedAt { get; set; }

        //relacion con suscrripciones
        public ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();

        //relacio con asistencia 
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();



    }
}