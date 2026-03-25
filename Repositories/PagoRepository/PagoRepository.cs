using Backend_Gimnacio.Data;
using Backend_Gimnacio.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gimnacio.Repositories.PagoRepository
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PagoRepository> _logger;

        public PagoRepository(ApplicationDbContext context, ILogger<PagoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // =========================
        // Obtener todos los pagos
        // =========================
        public async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Pagos
                .Include(p => p.Suscripcion)
                    .ThenInclude(s => s.Cliente)
                .ToListAsync();
        }

        // =========================
        // Obtener pago por ID
        // =========================
        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _context.Pagos
                .Include(p => p.Suscripcion)
                    .ThenInclude(s => s.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // =========================
        // Crear nuevo pago
        // =========================
        public async Task<Pago> AddAsync(Pago pago)
        {
            // 🔥 Validar que exista la suscripción
            var suscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.Id == pago.SuscripcionId);

            if (suscripcion == null)
            {
                _logger.LogWarning("Suscripción con ID {SuscripcionId} no encontrada", pago.SuscripcionId);
                throw new Exception($"Suscripción con ID {pago.SuscripcionId} no encontrada");
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            // 🔥 Recargar con includes
            var pagoCompleto = await GetByIdAsync(pago.Id);

            if (pagoCompleto == null)
            {
                _logger.LogError("Error al recuperar el pago creado");
                throw new Exception("Error al recuperar el pago creado");
            }

            return pagoCompleto;
        }

        // =========================
        // Actualizar pago (estado)
        // =========================
        public async Task<Pago?> UpdateAsync(Pago pago)
        {
            var existente = await _context.Pagos.FindAsync(pago.Id);

            if (existente == null)
                return null;

            // 🔥 SOLO actualizamos estado (tu regla de negocio)
            existente.Estado = pago.Estado;

            _context.Pagos.Update(existente);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(existente.Id);
        }

        // =========================
        // Eliminar pago
        // =========================
        public async Task<bool> DeleteAsync(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
                return false;

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}