using System.ComponentModel.DataAnnotations;
using Backend_Gimnacio.Enums;


namespace Backend_Gimnacio.Dtos
{
    public class MembresiaCreateDto
    {
        /// <example>Plan Semanal</example>
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        /// <example>Acceso completo al gimnasio</example>
        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; } = string.Empty;

        /// <example>7</example>
        [Range(1, int.MaxValue)]
        public int DuracionDias { get; set; }

        /// <example>Semanal</example>
        [Required(ErrorMessage = "El tipo de plan es requerido")]
        [EnumDataType(typeof(TipoPlan), ErrorMessage = "TipoPlan no válido.")]
        public TipoPlan TipoPlan { get; set; }

        /// <example>30</example>
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }
    }

    public class MembresiaUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? DuracionDias { get; set; }
        public TipoPlan? TipoPlan { get; set; }
        public decimal? Precio { get; set; }
        public bool? Estado { get; set; }
    }
    public class MembresiaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int DuracionDias { get; set; }
        public TipoPlan TipoPlan { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }


}