using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedisCacheAdapter.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Commands;
using VehicleTracking.ApplicationCore.Trackers.EventBus;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.BusService
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IEventBus _bus;
        private readonly ITrackingHistoryService _trackingHistoryService;

        public IServiceProvider _serviceProvider { get; }
        public SendMessageService(IEventBus bus, IServiceProvider serviceProvider,
            ITrackingHistoryService trackingHistoryService)
        {
            _bus = bus;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _trackingHistoryService = trackingHistoryService ?? throw new ArgumentNullException(nameof(trackingHistoryService));
        }
        public async Task<string> ProcessAsync(CreateLocationDto message)
        {
            await SendMessageAsync(message);
            return "true";
        }
        public async Task <TrackingViewModel> GetLocationAsync(string key)
        {
            var response = new TrackingViewModel();

            using var scope = _serviceProvider.CreateScope();
            var redisContext = scope.ServiceProvider.GetRequiredService<RedisCacheService>();

            if (redisContext.Any(key))
                return await Task.FromResult(JsonConvert.DeserializeObject<TrackingViewModel>(redisContext.Get<string>(key)));

            return await Task.FromResult(response);
        }
        public async Task<List<TrackingViewModel>> GetSpecificLocationAsync(LocationRequestDto request)
        {
            using var scope = _serviceProvider.CreateScope();
            var redisContext = scope.ServiceProvider.GetRequiredService<RedisCacheService>();

            var key = $"{request.DeviceId}{request.StartDate}{request.EndDate}";

            if (redisContext.Any(key))
                return await Task.FromResult(JsonConvert.DeserializeObject<List<TrackingViewModel>>(redisContext.Get<string>(key)));

            var trackingHistories = await _trackingHistoryService
                .GetAllHistoryByDateAsync(request);

            redisContext.Add(key, trackingHistories);

            return await Task.FromResult(trackingHistories);
        }

        public async Task SendMessageAsync(CreateLocationDto message)
        {
            var locationCommand = new CreateNewLocationCommand(
               message.VehicleId,
               message.DeviceId,
               message.Latitude,
               message.Longitude
           );

            await _bus.SendCommand(locationCommand);
        }
    }

}
