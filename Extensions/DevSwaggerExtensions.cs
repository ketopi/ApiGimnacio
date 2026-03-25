using Microsoft.AspNetCore.Builder;


namespace Backend_Gimnacio.Extensions
{
    public static class DevSwaggerExtensions
    {
        public static IApplicationBuilder UseDevSwagger(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseApiSwagger(); // Tu extensión existente para Swagger
            }


            return app;
        }
    }
}