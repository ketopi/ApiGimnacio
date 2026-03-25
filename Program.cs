using Backend_Gimnacio.Extensions;
using Backend_Gimnacio.Middleware;
using Backend_Gimnacio.Data;
using Microsoft.EntityFrameworkCore;
using Backend_Gimnacio.Services.LoginService;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Backend_Gimnacio.Security.Policies;
using Backend_Gimnacio.Security.Handlers;


var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Services
// ----------------------

builder.Services.AddRateLimitingServices(); // Rate limiting 

builder.Services.AddJwtAuthentication(builder.Configuration); // Autenticación JWT
builder.Services.AddAuthorization(); // Autorización (roles/policies)
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermisoPolicyProvider>(); // provider dinamico (policies) crean policies dinámicamente
builder.Services.AddSingleton<IAuthorizationHandler, PermisoHandler>();  // handler (policies) Contiene la lógica de validación

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));  // Mapea la configuración del JSON a la clase JwtSettings
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value); // Permite inyectar la configuración directamente sin usar IOptions

builder.Services.AddApiServices(); // Controladores + versionado
builder.Services.AddApplicationServices(); // Repositorios + servicios
builder.Services.AddSwaggerServices(); // Swagger + ConfigureSwaggerOptions + XML documentation
builder.Services.AddDevCors();    // CORS solo para Development

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));// Configura la conexión a la base de datos PostgreSQL

var app = builder.Build();

// ----------------------
// Middleware
// ----------------------

app.UseDevSwagger();            // Swagger solo en Development 
app.UseMiddleware<GlobalExceptionHandler>(); //captura todas las excepciones (middlewares y controladores) maneja errores
app.UseHttpsRedirection();
app.UseDevCorsMiddleware();     // CORS solo en Development   
app.UseRateLimiter();      // activar el middleware   
app.UseAuthentication();   //valida token
app.UseAuthorization(); // valida permisos
app.MapControllers(); //ejecuta los endpoints


// Crear/actualizar la base de datos automáticamente (solo para desarrollo) //// Crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment())
    {
        dbContext.Database.Migrate(); //EnsureCreated() o Migrate()
    }

}

app.Run();