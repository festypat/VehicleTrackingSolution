using System.Threading.Tasks;
using VehicleTracking.Tracking.Helper.Dto.Response;

namespace VehicleTracking.Infrastructure.Tracking.Interfaces
{
    public interface IGeoLocation
    {
        Task<GeoLocationResponseDto> GetLocation(double latitude, double longitude);
    }
}
