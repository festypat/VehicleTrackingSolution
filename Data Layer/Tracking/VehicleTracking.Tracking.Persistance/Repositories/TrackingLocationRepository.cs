using System;
using System.Threading.Tasks;
using VehicleTracking.Tracking.Domain.Entities;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.ViewModel;
using VehicleTracking.Tracking.Persistance.Context;

namespace VehicleTracking.Tracking.Persistance.Repositories
{
    public class TrackingLocationRepository
    {
        private readonly TrackingDbContext _context;
        public TrackingLocationRepository(TrackingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> GetLocationHistories(LocationRequestDto model)
        {
            try
            {


                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> LogLocationDetailsAsync(TrackingViewModel trackingViewModel)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var historyModel = new TrackingHistory
                    {
                        DeviceId = trackingViewModel.DeviceId,
                        DisplayName = trackingViewModel.DisplayName,
                        Latitude = trackingViewModel.Latitude,
                        Longitude = trackingViewModel.Longitude,
                        Licence = trackingViewModel.Licence,
                        PlaceId = trackingViewModel.PlaceId,
                        VehicleId = trackingViewModel.VehicleId
                    };

                    await _context.TrackingHistory.AddAsync(historyModel);
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
