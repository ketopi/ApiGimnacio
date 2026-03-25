using System.Threading.RateLimiting;

namespace Backend_Gimnacio.Extensions
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddRateLimitingServices(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("login", context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5, //maximo 5 intentos
                            Window = TimeSpan.FromMinutes(1), //por minuto
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        });
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}