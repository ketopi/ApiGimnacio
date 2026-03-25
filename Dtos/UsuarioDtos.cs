using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Dtos
{
    // crear usuario 
    public class UsuarioCreateDto
    {
        /// <example>1</example>
        [Required(ErrorMessage = "El empleado es requerido")]
        public int EmpleadoId { get; set; }

        /// <example>2</example>
        [Required(ErrorMessage = "El rol es requerido")]
        public int RolId { get; set; }

        /// <example>juan123</example>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        /// <example>123456</example>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }

    // actualizar usuario
    public class UsuarioUpdateDto
    {
        [Required]
        public int Id { get; set; }

        public int? RolId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }

        public bool? Estado { get; set; }
    }

    // respuesta
    public class UsuarioResponseDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public bool Estado { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UltimoAcceso { get; set; }

        public string RolNombre { get; set; } = string.Empty;

        public string EmpleadoNombre { get; set; } = string.Empty;
    }
}