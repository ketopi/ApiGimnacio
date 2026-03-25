

using Backend_Gimnacio.Repositories.AsistenciaRepository;
using Backend_Gimnacio.Repositories.ClienteRepository;
using Backend_Gimnacio.Repositories.EmpleadoRepository;
using Backend_Gimnacio.Repositories.MembresiaRepository;
using Backend_Gimnacio.Repositories.PagoRepository;
using Backend_Gimnacio.Repositories.PermisosRepository;
using Backend_Gimnacio.Repositories.SuscripcionRepository;
using Backend_Gimnacio.Repositories.UsuarioRepository;
using Backend_Gimnacio.Services.AsistenciaService;
using Backend_Gimnacio.Services.ClienteServices;
using Backend_Gimnacio.Services.EmpleadoService;
using Backend_Gimnacio.Services.LoginService;
using Backend_Gimnacio.Services.MembresiaServices;
using Backend_Gimnacio.Services.PagoService;
using Backend_Gimnacio.Services.PermisosServices;
using Backend_Gimnacio.Services.PermisosServices.PermisosService;
using Backend_Gimnacio.Services.SuscripcionService;
using Backend_Gimnacio.Services.UsuarioService;

namespace Backend_Gimnacio.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Permisos
            services.AddScoped<IPermisosService, PermisosService>();
            services.AddScoped<IPermisosRepository, PermisosRepository>();

            // Membresia
            services.AddScoped<IMembresiaService, MembresiaService>();
            services.AddScoped<IMembresiaRepository, MembresiaRepository>();

            //Cliente
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            //Empleado
            services.AddScoped<IEmpleadoService, EmpleadoService>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

            //Usuario
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Login
            services.AddScoped<LoginService, LoginService>();

            //suscripciones
            services.AddScoped<ISuscripcionService, SuscripcionService>();
            services.AddScoped<ISuscripcionRepository, SuscripcionRepository>();

            //asistencia
            services.AddScoped<IAsistenciaService, AsistenciaService>();
            services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();

            //pagos 
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<IPagoRepository, PagoRepository>();



            return services;
        }

    }
}