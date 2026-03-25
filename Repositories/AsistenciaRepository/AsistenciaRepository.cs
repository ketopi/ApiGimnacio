using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend_Gimnacio.Repositories.AsistenciaRepository
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AsistenciaRepository> _logger;

        public AsistenciaRepository(ApplicationDbContext context, ILogger<AsistenciaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtener todas las asistencias
        public async Task<List<Asistencia>> GetAllAsync()
        {
            return await _context.Asistencias
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Cliente)
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Membresia)
                .Include(a => a.RegistradoPor)
                    .ThenInclude(u => u.Empleado)
                .ToListAsync();
        }

        // Obtener asistencias por cliente (ahora vía Suscripción)
        public async Task<List<Asistencia>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Asistencias
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Cliente)
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Membresia)
                .Include(a => a.RegistradoPor)
                    .ThenInclude(u => u.Empleado)
                .Where(a => a.Suscripcion.ClienteId == clienteId) // 🔥 CORRECTO
                .ToListAsync();
        }

        // Agregar nueva asistencia
        public async Task<Asistencia> AddAsync(Asistencia asistencia)
        {
            // 🔥 Validar Suscripción
            var suscripcion = await _context.Suscripciones
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(s => s.Id == asistencia.SuscripcionId);

            if (suscripcion == null)
            {
                _logger.LogWarning("Suscripción con ID {SuscripcionId} no encontrada.", asistencia.SuscripcionId);
                throw new Exception($"Suscripción con ID {asistencia.SuscripcionId} no encontrada.");
            }

            // 🔥 Validación PRO (muy importante)
            if (suscripcion.FechaFin < DateTime.UtcNow)
            {
                _logger.LogWarning("Intento de asistencia con suscripción vencida. ClienteId: {ClienteId}", suscripcion.ClienteId);
                throw new Exception("La suscripción está vencida.");
            }

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            // 🔥 Recargar completo
            var asistenciaCompleta = await _context.Asistencias
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Cliente)
                .Include(a => a.Suscripcion)
                    .ThenInclude(s => s.Membresia)
                .Include(a => a.RegistradoPor)
                    .ThenInclude(u => u.Empleado)
                .FirstOrDefaultAsync(a => a.Id == asistencia.Id);

            if (asistenciaCompleta == null)
            {
                _logger.LogError("No se pudo recuperar la asistencia creada.");
                throw new Exception("Error al recuperar la asistencia.");
            }

            return asistenciaCompleta;
        }
    }
}