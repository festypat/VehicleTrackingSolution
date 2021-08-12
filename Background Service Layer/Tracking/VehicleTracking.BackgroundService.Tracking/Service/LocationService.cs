using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VehicleTracking.BackgroundService.Tracking.Interfaces;
using VehicleTracking.Infrastructure.Tracking.Interfaces;
using VehicleTracking.Tracking.Domain.Entities;
using VehicleTracking.Tracking.Persistance.Context;
using VehicleTracking.Tracking.Persistance.Repositories;

namespace VehicleTracking.BackgroundService.Tracking.Service
{
    public class LocationService : ILocationService
    {
        private readonly IGeoLocation _geoLocation;
        private readonly TrackingLocationRepository _trackingLocationRepository;
        public LocationService(IServiceProvider services, IGeoLocation geoLocation,
            TrackingLocationRepository trackingLocationRepository)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _geoLocation = geoLocation ?? throw new ArgumentNullException(nameof(geoLocation));
            _trackingLocationRepository = trackingLocationRepository ?? throw new ArgumentNullException(nameof(trackingLocationRepository));
        }
        public IServiceProvider Services { get; }
        public async Task<string> GetCurrentLocation()
        {
            try
            {
                using var scope = Services.CreateScope();
                var locationContext = scope.ServiceProvider.GetRequiredService<TrackingDbContext>();
                //var locations = await locationContext.GetAllAsync();
                var locations = await locationContext.Location.ToListAsync();

                foreach (var result in locations)
                {
                    using (var transaction = await locationContext.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var findLocation = await locationContext.Location
                                .SingleOrDefaultAsync(x => x.LocationId == result.LocationId);

                            var historyModel = new TrackingHistory
                            {
                                DeviceId = result.DeviceId,
                                DisplayName = result.DisplayName,
                                Latitude = result.Latitude,
                                Longitude = result.Longitude,
                                Licence = result.Licence,
                                PlaceId = result.PlaceId,
                                VehicleId = result.VehicleId
                            };

                            await locationContext.TrackingHistory.AddAsync(historyModel);
                            await locationContext.SaveChangesAsync();

                            var getLocation = await _geoLocation.GetLocation(findLocation.Latitude, findLocation.Longitude);

                            if (getLocation.responseCode == "00")
                            {
                                findLocation.DisplayName = getLocation.display_name;
                                findLocation.PlaceId = getLocation.place_id;
                                findLocation.Licence = getLocation.licence;

                                locationContext.Update(findLocation);
                                await locationContext.SaveChangesAsync();
                                await transaction.CommitAsync();
                            }

                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }

                }

                return "Task Completed";
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }
    }


}
