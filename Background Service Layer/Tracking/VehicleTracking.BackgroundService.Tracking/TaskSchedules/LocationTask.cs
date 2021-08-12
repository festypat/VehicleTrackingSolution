using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.BackgroundService.Tracking.BackgroundServices;
using VehicleTracking.BackgroundService.Tracking.Interfaces;

namespace VehicleTracking.BackgroundService.Tracking.TaskSchedules
{
    public class LocationTask : CronJobService
    {
        private readonly IServiceProvider _scopeServiceProvider;

        public LocationTask(IServiceProvider serviceProvider, IScheduleConfig<LocationTask> config) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _scopeServiceProvider = serviceProvider;
        }

        public override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeServiceProvider.CreateScope();

            ILocationService locations = scope.ServiceProvider.GetRequiredService<ILocationService>();
            locations.GetCurrentLocation();

            return Task.CompletedTask;
        }
    }

}
