using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.Interfaces.Service
{
    public interface ITrackingService
    {
        Task<List<TrackingViewModel>> GetAllAsync();
        Task<TrackingViewModel> GetTrackingInfo(long trackingId);
        Task<int> CountTotalTrackingAsync();
        Task<bool> ExistsAsync(long trackingId);
        Task<TrackingViewModel> AddAsync(TrackingViewModel model);
        Task UpdateAsync(TrackingViewModel model);
        Task DeleteAsync(int id);
    }
}
