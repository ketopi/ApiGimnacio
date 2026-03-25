using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Mappers;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Repositories.PagoRepository;
using Microsoft.Extensions.Logging;

namespace Backend_Gimnacio.Services.PagoService
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly ILogger<PagoService> _logger;

        public PagoService(IPagoRepository pagoRepository, ILogger<PagoService> logger)
        {
            _pagoRepository = pagoRepository;
            _logger = logger;
        }

        // =========================
        // Obtener todos los pagos
        // =========================
        public async Task<IEnumerable<PagoResponseDto>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllAsync();
            return pagos.ToResponseDtoList();
        }

        // =========================
        // Obtener pago por ID
        // =========================
        public async Task<PagoResponseDto?> GetByIdAsync(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return null;

            return pago.ToResponseDto();
        }

        // =========================
        // Crear pago
        // =========================
        public async Task<PagoResponseDto> AddAsync(PagoCreateDto dto)
        {
            var pago = dto.ToEntity();

            var created = await _pagoRepository.AddAsync(pago);

            _logger.LogInformation(
                "Pago creado | Id: {PagoId} | SuscripcionId: {SuscripcionId} | Monto: {Monto}",
                created.Id,
                created.SuscripcionId,
                created.Monto
            );

            return created.ToResponseDto();
        }

        // =========================
        // Actualizar solo estado
        // =========================
        public async Task<PagoResponseDto?> UpdateEstadoAsync(int id, PagoUpdateDto dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return null;

            // 🔥 regla: solo estado
            if (pago.Estado == dto.Estado)
            {
                _logger.LogWarning("El pago {PagoId} ya tiene el estado {Estado}", id, dto.Estado);
                throw new Exception("El pago ya tiene ese estado");
            }

            pago.Estado = dto.Estado;

            var actualizado = await _pagoRepository.UpdateAsync(pago);

            return actualizado?.ToResponseDto();
        }

        // =========================
        // Eliminar pago
        // =========================
        public async Task<bool> DeleteAsync(int id)
        {
            var eliminado = await _pagoRepository.DeleteAsync(id);

            if (!eliminado)
            {
                _logger.LogWarning("Pago con ID {PagoId} no encontrado para eliminar", id);
                return false;
            }

            _logger.LogInformation("Pago con ID {PagoId} eliminado correctamente", id);
            return true;
        }
    }
}