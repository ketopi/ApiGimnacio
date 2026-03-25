
using Microsoft.AspNetCore.Authorization;

namespace Backend_Gimnacio.Security.Requirements
{
    //Es una regla de autorización (dominio de seguridad)
    public class PermisoRequirement : IAuthorizationRequirement
    {
        public string Permiso { get; }

        public PermisoRequirement(string permiso)
        {
            Permiso = permiso;
        }

    }
}