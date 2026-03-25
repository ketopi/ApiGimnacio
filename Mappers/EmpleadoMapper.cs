using System;
using System.Collections.Generic;
using System.Linq;
using Backend_Gimnacio.Models;
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Mappers
{

    /// Mapper profesional para convertir entre Empleado y sus DTOs.
    public static class EmpleadoMapper
    {

        /// Convierte una entidad Empleado a EmpleadoResponseDto.
        public static EmpleadoResponseDto ToResponseDto(this Empleado empleado)
        {
            return new EmpleadoResponseDto
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                AP = empleado.AP,
                AM = empleado.AM,
                Celular = empleado.Celular,
                Estado = empleado.Estado,
                CreatedAt = empleado.CreatedAt
            };
        }


        // Convierte una lista de Empleados a lista de DTOs.
        public static IEnumerable<EmpleadoResponseDto> ToResponseDtoList(this IEnumerable<Empleado> empleados)
        {
            return empleados.Select(e => e.ToResponseDto());
        }


        /// Convierte un EmpleadoCreateDto a entidad Empleado.
        public static Empleado ToEntity(this EmpleadoCreateDto dto)
        {
            return new Empleado
            {
                Nombre = dto.Nombre,
                AP = dto.AP,
                AM = dto.AM,
                Celular = dto.Celular,
                Estado = true,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }


        /// Actualiza una entidad existente con datos del DTO.
        public static void UpdateEntity(this Empleado empleado, EmpleadoUpdateDto dto)
        {
            empleado.Nombre = dto.Nombre;
            empleado.AP = dto.AP;
            empleado.AM = dto.AM;
            empleado.Celular = dto.Celular;
            empleado.Estado = dto.Estado;
        }
    }
}