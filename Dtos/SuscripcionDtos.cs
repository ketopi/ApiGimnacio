namespace Backend_Gimnacio.Dtos
{
    // Para crear una suscripción
    public class SuscripcionCreateDto
    {
        public int ClienteId { get; set; }
        public int MembresiaId { get; set; }
   
    }

    // Para actualizar una suscripción
    public class SuscripcionUpdateDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; }
    }

    // Para responder al frontend
    public class SuscripcionResponseDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;

        public int MembresiaId { get; set; }
        public string MembresiaNombre { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}