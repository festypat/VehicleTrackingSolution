using Helper.Layer.Identity.Notification;
using Microsoft.Extensions.DependencyInjection;
using VehicleTracking.ApplicationCore.Identity.Jwt;
using VehicleTracking.ApplicationCore.Identity.Services;
using VehicleTracking.ApplicationCore.Identity.Services.IdentityService;
using VehicleTracking.ApplicationCore.Identity.Services.Interface;

namespace Infra.Ioc.Identity.Container
{
    public static class IdentityDependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<TokenService>();
            services.AddScoped<IdentityRepository>();
            services.AddScoped<INotificationIdentityTask, NotificationIdentityTask>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();

            return services;
        }
    }
}
