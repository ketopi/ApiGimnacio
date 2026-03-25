using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend_Gimnacio.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo
            {
                Title = $"Gym API {description.GroupName}",
                Version = description.ApiVersion.ToString(),
                Description = "API RESTful para la gestión integral de un gimnasio. Permite administrar miembros, entrenadores, rutinas de entrenamiento, planes de membresía, control de asistencia y pagos del sistema.",
                Contact = new OpenApiContact
                {
                    Name = "Kevin Torrez Pillco (Developer)",
                    Email = "totokevin6@gmail.com",
                    Url = new Uri("https://ketopi.github.io/Portafolio/")
                },
                License = new OpenApiLicense
                {
                    Name = "Licencia de uso del sistema Gym Management",
                    Url = new Uri("https://example.com/license")
                },
                TermsOfService = new Uri("https://example.com/terms"),
            };

            if (description.IsDeprecated)
            {
                info.Description = $@"
                **⚠️ IMPORTANTE: Versión {description.ApiVersion} Deprecada**

                Esta versión de la API del sistema de gestión de gimnasio ha sido marcada como obsoleta y será retirada en el futuro.

                **📅 Fecha estimada de eliminación:** {DateTime.Now.AddMonths(6):dd/MM/yyyy}

                **🆕 Acción recomendada:** Actualizar las integraciones a la versión más reciente disponible.  
                **📚 Documentación de la nueva versión:** /swagger/v2

                Para más información o soporte técnico, contacta al equipo de desarrollo.

                ---
                {info.Description}";
            }

            options.SwaggerDoc(description.GroupName, info);
        }

        //Configuracion de seguridad JWT para el swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese el token JWT así: Bearer {token}"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
         });
    }
}