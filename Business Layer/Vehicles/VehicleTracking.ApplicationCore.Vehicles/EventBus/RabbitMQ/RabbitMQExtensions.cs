using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        //public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration Configuration)
        //{
        //    var options = new RabbitMQOptions();
        //    Configuration.GetSection(nameof(RabbitMQOptions)).Bind(options);
        //    services.Configure<RabbitMQOptions>(Configuration.GetSection(nameof(RabbitMQOptions)));

        //    services.AddRawRabbit(new RawRabbitOptions
        //    {
        //        ClientConfiguration = options
        //    });

        //    services.AddSingleton<IEventListener, RabbitMQListener>();

        //    return services;
        //}

        public static IApplicationBuilder UseRabbitMQSubscribe<T>(this IApplicationBuilder app) where T : IEvent
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<T>();

            return app;
        }
    }

}
