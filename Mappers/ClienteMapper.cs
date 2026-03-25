using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Models;

namespace Backend_Gimnacio.Mappers
{
    public static class ClienteMapper
    {
        // Modelo → ResponseDto (para devolver al frontend)
        public static ClienteResponseDto ToResponseDto(this Cliente cliente)
        {
            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                AP = cliente.AP,
                AM = cliente.AM,
                CI = cliente.CI,
                Celular = cliente.Celular,
                Estado = cliente.Estado,
                CreatedAt = cliente.CreatedAt
            };
        }

        // CreateDto → Modelo (para guardar en BD)
        public static Cliente ToModel(this ClienteCreateDto dto)
        {
            return new Cliente
            {
                Nombre = dto.Nombre,
                AP = dto.AP,
                AM = dto.AM ?? string.Empty,
                CI = dto.CI,
                Celular = dto.Celular
            };
        }

        // UpdateDto → Modelo existente (solo actualiza campos enviados)
        public static void UpdateModel(this ClienteUpdateDto dto, Cliente cliente)
        {
            cliente.Nombre = dto.Nombre?? cliente.Nombre;
            cliente.AP = dto.AP?? cliente.AP;
            cliente.AM = dto.AM?? cliente.AM;
            cliente.CI = dto.CI?? cliente.CI;
            cliente.Celular = dto.Celular?? cliente.Celular;
            cliente.Estado = dto.Estado?? cliente.Estado;
        }

        // Lista de modelos → Lista de ResponseDtos
        public static IEnumerable<ClienteResponseDto> ToResponseDtoList(this IEnumerable<Cliente> clientes)
        {
            return clientes.Select(c => c.ToResponseDto());
        }
    }
}