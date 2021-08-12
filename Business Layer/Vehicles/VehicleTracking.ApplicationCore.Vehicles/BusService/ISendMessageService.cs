using System.Threading.Tasks;
using VehicleTracking.Vehicle.Helper.Dto.Request;

namespace VehicleTracking.ApplicationCore.Vehicles.BusService
{
    public interface ISendMessageService
    {
        Task SendMessageAsync(CreateVehicleDto message);
        Task<string> ProcessAsync(CreateVehicleDto message);
    }
}
