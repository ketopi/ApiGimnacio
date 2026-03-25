using System;
using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Models
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [StringLength(50, ErrorMessage = "El apellido paterno no puede exceder 50 caracteres")]
        public string AP { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido materno es requerido")]
        [StringLength(50, ErrorMessage = "El apellido materno no puede exceder 50 caracteres")]
        public string AM { get; set; } = string.Empty;

        [Required(ErrorMessage = "El celular es requerido")]
        [Phone(ErrorMessage = "El número de celular no es válido")]       
        public string Celular { get; set; } = string.Empty;

        [Required]
        public bool Estado { get; set; } = true;

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


        public Usuario Usuario { get; set; } = null!;
    }
}