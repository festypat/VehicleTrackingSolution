using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.Infrastructure.Tracking.Interfaces;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.BackgroundService.Tracking.Repository
{
    public class LocationRepository
    {
        private readonly ITrackingService _trackingService;
        private readonly IGeoLocation _geoLocation;
        public LocationRepository(ITrackingService trackingService, IGeoLocation geoLocation,
            IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
            _geoLocation = geoLocation ?? throw new ArgumentNullException(nameof(geoLocation));
        }

        public IServiceProvider Services { get; }
        public async Task ProcessLocationRequests(List<TrackingViewModel> locations)
        {
            try
            {
                IServiceCollection collectionService = new ServiceCollection();

                var serviceProvider = collectionService.BuildServiceProvider();

                var locationContext = serviceProvider.GetRequiredService<ITrackingService>();


                foreach (var result in locations)
                {
                    var getVehicle = await locationContext.GetTrackingInfo(result.LocationId);

                    var getLocation = await _geoLocation.GetLocation(getVehicle.Latitude, getVehicle.Longitude);

                    if (getLocation.responseCode == "00")
                    {
                        getVehicle.DisplayName = getLocation.display_name;
                        getVehicle.PlaceId = getLocation.place_id;
                        getVehicle.Licence = getLocation.licence;

                        await locationContext.UpdateAsync(getVehicle);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
