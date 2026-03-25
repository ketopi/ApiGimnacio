
using Backend_Gimnacio.Security.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Backend_Gimnacio.Security.Policies
{
    //Define cómo se crean policies dinámicamente
    public class PermisoPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermisoPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // 🔥 Crea policy dinámicamente
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermisoRequirement(policyName))
                .Build();

            return await Task.FromResult(policy);
        }


    }
}