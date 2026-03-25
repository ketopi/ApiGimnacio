using System.ComponentModel.DataAnnotations;

namespace Backend_Gimnacio.Dtos
{    
    public class PermisoCreateDto
    {

        /// <example>usuarios.ver</example>
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;


        /// <example>Permite visualizar la lista de usuarios del sistema</example>
        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
    }

    public class PermisoUpdateDto
    {

        [StringLength(50)]
        public string? Nombre { get; set; }

        [StringLength(200)]
        public string? Descripcion { get; set; }
    }


    public class PermisoResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}