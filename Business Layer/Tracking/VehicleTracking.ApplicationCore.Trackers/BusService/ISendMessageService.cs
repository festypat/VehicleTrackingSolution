using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.BusService
{
    public interface ISendMessageService
    {
        Task SendMessageAsync(CreateLocationDto message);
        Task<string> ProcessAsync(CreateLocationDto message);
        Task<TrackingViewModel> GetLocationAsync(string key);
        Task <List<TrackingViewModel>> GetSpecificLocationAsync(LocationRequestDto location);
    }
}
