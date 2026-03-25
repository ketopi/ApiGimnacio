using Backend_Gimnacio.Models;


namespace Backend_Gimnacio.Repositories.PermisosRepository
{
    public interface IPermisosRepository
    {
        Task<IEnumerable<Permisos>> GetPermisosAsync();
        Task<Permisos?> GetPermisoByIdAsync(int id);
        Task<Permisos> CreateAsync(Permisos permiso);     
        
    }
}