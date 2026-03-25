using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Dtos
{
    public class ClienteCreateDto
    {
        /// <example>Kevin</example>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        /// <example>Torrez</example>
        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El apellido paterno no puede exceder 100 caracteres.")]
        public string AP { get; set; } = string.Empty;

        /// <example>Pillco</example>
        [MaxLength(100, ErrorMessage = "El apellido materno no puede exceder 100 caracteres.")]
        public string? AM { get; set; }

        /// <example>12780872</example>
        [Required(ErrorMessage = "El CI es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El CI no puede exceder 20 caracteres.")]
        public string CI { get; set; } = string.Empty;

        /// <example>63945700</example>
        [Required(ErrorMessage = "El celular es obligatorio.")]
        public int Celular { get; set; }
    }

    public class ClienteUpdateDto
    {
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string? Nombre { get; set; }

        [MaxLength(100, ErrorMessage = "El apellido paterno no puede exceder 100 caracteres.")]
        public string? AP { get; set; }

        [MaxLength(100, ErrorMessage = "El apellido materno no puede exceder 100 caracteres.")]
        public string? AM { get; set; }

        [MaxLength(20, ErrorMessage = "El CI no puede exceder 20 caracteres.")]
        public string? CI { get; set; }

        [Range(60000000, 79999999, ErrorMessage = "El celular debe ser un número boliviano válido.")]
        public int? Celular { get; set; }

        public bool? Estado { get; set; }
    }

    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string AP { get; set; } = string.Empty;
        public string? AM { get; set; }
        public string CI { get; set; } = string.Empty;
        public int Celular { get; set; }
        public bool Estado { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}