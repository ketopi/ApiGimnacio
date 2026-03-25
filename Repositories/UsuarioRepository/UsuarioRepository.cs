using Backend_Gimnacio.Data;

using Microsoft.EntityFrameworkCore;

namespace Backend_Gimnacio.Repositories.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los usuarios
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .ToListAsync();
        }

        // Obtener usuario por ID
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Crear usuario
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        // Actualizar usuario
        public async Task<Usuario?> UpdateAsync(Usuario usuario)
        {
            var existing = await _dbContext.Usuarios.FindAsync(usuario.Id);

            if (existing == null)
                return null;

            existing.Username = usuario.Username;
            existing.RolId = usuario.RolId;
            existing.Estado = usuario.Estado;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        // Eliminar usuario (soft delete)
        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
                return false;

            usuario.Estado = false; //  borrado lógico
            await _dbContext.SaveChangesAsync();

            return true;
        }

        //obetener por username para login
        public async Task<Usuario?> GetUsuarioByNombreAsync(string username)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Username == username);
        }


        // Trae todos los permisos de usuario ** IMPORTANTE
        public async Task<Usuario?> GetUserWithPermissionsAsync(string username)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Rol)
                    .ThenInclude(r => r.RolPermisos)
                        .ThenInclude(rp => rp.Permiso)
                .Include(u => u.Empleado) 
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}