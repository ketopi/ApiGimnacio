using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Models
{
    public class Permisos
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } // fecha, hora Y el desfase de zona horaria

        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    

    }
}