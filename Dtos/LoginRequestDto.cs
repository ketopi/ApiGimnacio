using System.ComponentModel.DataAnnotations;


namespace Backend_Gimnacio.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string RolNombre { get; set; } = string.Empty;
        public string EmpleadoNombre { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset? UltimoAcceso { get; set; }
        public List<string> Permisos { get; internal set; } = new List<string>(); //para ver los permisos solo en desarrollo (debug)
    
    }
}