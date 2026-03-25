using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Backend_Gimnacio.Dtos;
using Backend_Gimnacio.Repositories.UsuarioRepository;

namespace Backend_Gimnacio.Services.LoginService
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
    }

    public class LoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto, JwtSettings jwtSettings)
        {
            //  Buscar usuario
            var usuario = await _usuarioRepository.GetUserWithPermissionsAsync(loginDto.Username);
            if (usuario == null) return null;

            // Verificar contraseña
            bool isValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.PasswordHash);
            if (!isValid) return null;

            // Actualizar último acceso
            usuario.UltimoAcceso = DateTimeOffset.UtcNow;
            await _usuarioRepository.UpdateAsync(usuario);

            // Generar token JWT
            var token = GenerateJwtToken(usuario, jwtSettings);

            // Retornar DTO
            return new LoginResponseDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                RolNombre = usuario.Rol.Nombre,
                EmpleadoNombre = usuario.Empleado.Nombre,
                UltimoAcceso = usuario.UltimoAcceso,
                Token = token,

                //solo para debug
                Permisos = usuario.Rol.RolPermisos
                .Select(rp => rp.Permiso.Nombre)
                .ToList()
            };
        }

        private string GenerateJwtToken(Usuario usuario, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var rp in usuario.Rol.RolPermisos)
            {
                claims.Add(new Claim("permiso", rp.Permiso.Nombre.Trim()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Calcula expiración a las 22:00 del mismo día
            var ahora = DateTime.UtcNow;
            var expiracion = new DateTime(ahora.Year, ahora.Month, ahora.Day, 22, 0, 0, DateTimeKind.Utc);
            if (ahora > expiracion)
            {
                // Si ya pasamos de las 22:00, expira al día siguiente a las 22:00
                expiracion = expiracion.AddDays(1);
            }

            var token = new JwtSecurityToken(
                issuer: "BackendGimnacio",
                audience: "BackendGimnacioUsers",
                claims: claims,
                expires: expiracion,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}