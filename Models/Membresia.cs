using System.ComponentModel.DataAnnotations;
using Backend_Gimnacio.Enums;

namespace Backend_Gimnacio.Models
{
    public class Membresia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser mayor a 0")]
        public int DuracionDias { get; set; }

        // 🔥 Nuevo campo PRO
        [Required(ErrorMessage = "El tipo de plan es requerido")]
        public TipoPlan TipoPlan { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
        public decimal Precio { get; set; }
        public bool Estado { get; set; } = true;

        

        //relacion con suscripciones
        public ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();
    }
}