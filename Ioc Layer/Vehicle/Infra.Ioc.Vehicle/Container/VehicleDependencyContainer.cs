using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.ApplicationCore.Vehicles.BusService;
using VehicleTracking.ApplicationCore.Vehicles.Commands;
using VehicleTracking.ApplicationCore.Vehicles.EventBus;
using VehicleTracking.ApplicationCore.Vehicles.Handlers;
using VehicleTracking.ApplicationCore.Vehicles.Interfaces.Repositories;
using VehicleTracking.ApplicationCore.Vehicles.Interfaces.Service;
using VehicleTracking.ApplicationCore.Vehicles.Services;
using VehicleTracking.Vehicle.Helper.Notification;
using VehicleTracking.Vehicle.Persistance.Repositories;

namespace Infra.Ioc.Vehicle.Container
{
    public static class VehicleDependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<INotificationVehicleTask, NotificationVehicleTask>();

            services.AddTransient<IEventBus, EventMQBus>();

            services.AddTransient<ISendMessageService, SendMessageService>();

            services.AddTransient<IRequestHandler<CreateNewVehicleCommand, bool>, VehicleHandler>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));


            return services;
        }

    }

}
