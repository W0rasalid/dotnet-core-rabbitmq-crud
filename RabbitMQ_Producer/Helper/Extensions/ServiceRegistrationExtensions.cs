using RabbitMQ_Producer.Repositories;
using RabbitMQ_Producer.Services;

namespace RabbitMQ_Producer.Helper.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<SystemLogService, SystemLogService>();


        }

        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<SystemLogRepository, SystemLogRepository>();

        }
    }
}
