using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Dtos;

namespace Backend_Gimnacio.Services.UsuarioService
{
    public interface IUsuarioService
    {

        Task<IEnumerable<UsuarioResponseDto>> GetAllUsuariosAsync();
        Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id);
        Task<UsuarioResponseDto> CreateUsuarioAsync(UsuarioCreateDto crearUsuarioDto);
        Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateDto updateUsuarioDto, string? hashedPassword = null);
        Task<bool> DeleteAsync(int id);
        
        
    }
}