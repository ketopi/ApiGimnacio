using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Gimnacio.Security.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Gimnacio.Security.Handlers
{
    //Contiene la lógica de validación
    public class PermisoHandler : AuthorizationHandler<PermisoRequirement>
    {
        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermisoRequirement requirement)
        {
            var permisos = context.User.Claims
                .Where(c => c.Type == "permiso")
                .Select(c => c.Value.Trim()); // limpia espacio y \n

            if (permisos.Any(p => p == requirement.Permiso))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}