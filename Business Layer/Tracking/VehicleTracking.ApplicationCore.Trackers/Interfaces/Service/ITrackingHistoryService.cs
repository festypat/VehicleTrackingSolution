using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.Interfaces.Service
{
    public interface ITrackingHistoryService
    {
        Task<List<TrackingViewModel>> GetAllAsync();
        Task<List<TrackingViewModel>> GetAllHistoryByDateAsync(LocationRequestDto model);
        Task<TrackingViewModel> GetHistoryInfo(long historyId);
        Task<bool> ExistsAsync(long historyId);
        Task<TrackingViewModel> AddAsync(TrackingViewModel model);
    }
}
