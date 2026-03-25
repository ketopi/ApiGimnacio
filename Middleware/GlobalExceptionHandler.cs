using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Primitives;


namespace Backend_Gimnacio.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Generar o recuperar CorrelationId
            context.Request.Headers.TryGetValue("X-Correlation-ID", out StringValues correlationIdValues);
            string correlationId = correlationIdValues.FirstOrDefault() ?? Guid.NewGuid().ToString();

            context.Response.Headers["X-Correlation-ID"] = correlationId;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, correlationId);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
        {
            _logger.LogError(exception, "Excepción no controlada: {Message}, CorrelationId: {CorrelationId}", exception.Message, correlationId);

            // Valores por defecto
            var statusCode = HttpStatusCode.InternalServerError;
            var detailMessage = "Error interno del servidor";

            // Mapear excepciones específicas
            switch (exception)
            {
                case ValidationException ve:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    detailMessage = ve.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    detailMessage = "No autorizado";
                    break;

                case ForbiddenAccessException:
                    statusCode = HttpStatusCode.Forbidden;
                    detailMessage = "Acceso prohibido";
                    break;

                case RoleNotAssignedException:
                    statusCode = HttpStatusCode.Forbidden;

                    detailMessage = "Rol no asignado";
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    detailMessage = "Recurso no encontrado";
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    detailMessage = exception.Message;
                    break;
            }

            // Construir ProblemDetails estándar
            var problemDetails = new ProblemDetails
            {

                Title = statusCode.ToString(),
                Status = (int)statusCode,
                Detail = detailMessage,
                Instance = context.Request.Path
            };

            // Información adicional solo en desarrollo
            if (_env.IsDevelopment())
            {
                problemDetails.Extensions["correlationId"] = correlationId;
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                problemDetails.Extensions["message"] = exception.Message;
                problemDetails.Extensions["innerException"] = exception.InnerException?.Message;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            await context.Response.WriteAsync(json);
        }
    }

    // Excepción personalizada para Forbidden
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message) : base(message) { }
    }

    // Usuario no autenticado
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message) : base(message) { }
    }

    // Usuario sin rol asignado
    public class RoleNotAssignedException : Exception
    {
        public RoleNotAssignedException(string message) : base(message) { }
    }
}