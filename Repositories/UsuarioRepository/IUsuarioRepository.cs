

namespace Backend_Gimnacio.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);        
        Task<Usuario?> GetUsuarioByNombreAsync(string username);
        
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<Usuario?> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        
        Task<Usuario?> GetUserWithPermissionsAsync(string username);
    
        
    }
}