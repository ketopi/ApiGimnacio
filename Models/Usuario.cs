using System.ComponentModel.DataAnnotations;
using Backend_Gimnacio.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    public int RolId { get; set; }
    public Roles Rol { get; set; } = null!;

    public bool Estado { get; set; } = true;

    public DateTimeOffset? UltimoAcceso { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    //Relacion con asistencia
    public ICollection<Asistencia> AsistenciasRegistradas { get; set; } = new List<Asistencia>();

}