using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_Gimnacio.Extensions
{
    public static class DevCorsExtensions
    {
        public static IServiceCollection AddDevCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseDevCorsMiddleware(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseCors("DevPolicy");
            }


            return app;
        }

    }
}