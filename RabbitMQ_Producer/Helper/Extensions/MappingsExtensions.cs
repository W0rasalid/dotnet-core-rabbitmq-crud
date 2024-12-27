using RabbitMQ_Producer.Helper.Mappings;

namespace RabbitMQ_Producer.Helper.Extensions
{
    public static class MappingsExtensions
    {
        public static void AddApplicationMappings(this IServiceCollection services)
        {
            // Add AutoMapper services to the container.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SystemLogProfile>();
            });

        }
    }
}
