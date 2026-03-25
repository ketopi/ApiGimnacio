using System;
using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Dtos
{

    public class EmpleadoCreateDto
    {
        /// <example>Juan</example>
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        /// <example>Pérez</example>
        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [StringLength(50)]
        public string AP { get; set; } = string.Empty;

        /// <example>Gómez</example>
        [Required(ErrorMessage = "El apellido materno es requerido")]
        [StringLength(50)]
        public string AM { get; set; } = string.Empty;

        /// <example>77712345</example>
        [Required(ErrorMessage = "El celular es requerido")]
        [Phone(ErrorMessage = "El número de celular no es válido")]
        public string Celular { get; set; } = string.Empty;
    }


    public class EmpleadoUpdateDto
    {

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nombre { get; set; } = string.Empty;


        [Required]
        [StringLength(50)]
        public string AP { get; set; } = string.Empty;


        [Required]
        [StringLength(50)]
        public string AM { get; set; } = string.Empty;


        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El celular debe tener 8 dígitos")]
        public string Celular { get; set; } = string.Empty;


        public bool Estado { get; set; }
    }

    public class EmpleadoResponseDto
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;


        public string AP { get; set; } = string.Empty;


        public string AM { get; set; } = string.Empty;


        public string Celular { get; set; } = string.Empty;


        public bool Estado { get; set; }


        public DateTimeOffset CreatedAt { get; set; }
    }
}