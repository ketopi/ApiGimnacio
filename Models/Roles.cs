
using System.ComponentModel.DataAnnotations;


namespace Backend_Gimnacio.Models
{
    public class Roles
    {

        [Key]
        public int Id { get; set; }

        [Required]
        
        public string Nombre { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Relación muchos a muchos con Permisos
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
        //relacion 1 a muchos
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    
        
    }
}