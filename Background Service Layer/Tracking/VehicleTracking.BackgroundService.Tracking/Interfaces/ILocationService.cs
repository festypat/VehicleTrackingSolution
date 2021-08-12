using System.Threading.Tasks;

namespace VehicleTracking.BackgroundService.Tracking.Interfaces
{
    public interface ILocationService
    {
        Task<string> GetCurrentLocation();
    }
}
