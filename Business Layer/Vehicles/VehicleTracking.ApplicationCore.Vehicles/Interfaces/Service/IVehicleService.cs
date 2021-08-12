using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.Vehicle.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Vehicles.Interfaces.Service
{
    public interface IVehicleService
    {
        Task<List<VehicleViewModel>> GetAllAsync();
        Task<VehicleViewModel> GetVehicleInfo(long vehicleId);
        Task<VehicleViewModel> GetVehicleName(string vehicleName);
        Task<int> CountTotalVehiclesAsync();
        Task<bool> ExistsAsync(string licenceNumber);
        Task <VehicleViewModel> AddAsync(VehicleViewModel model);
        Task UpdateAsync(VehicleViewModel model);
        Task DeleteAsync(int id);
    }
}
