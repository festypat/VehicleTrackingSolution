using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.Interfaces.Repositories;
using VehicleTracking.ApplicationCore.Vehicles.Interfaces.Service;
using VehicleTracking.Vehicle.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Vehicles.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Vehicle.Domain.Entities.Vehicle> _vehicle;

        public VehicleService(IAsyncRepository<Vehicle.Domain.Entities.Vehicle> vehicle)
        {
            _vehicle = vehicle;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle.Domain.Entities.Vehicle, VehicleViewModel>());

            _mapper = config.CreateMapper();
        }

        public async Task<List<VehicleViewModel>> GetAllAsync()
        {
            var vehicles = await _vehicle.GetAllAsync();

            return _mapper.Map<List<Vehicle.Domain.Entities.Vehicle>, List<VehicleViewModel>>(vehicles);
        }

        public async Task<VehicleViewModel> GetVehicleInfo(long vehicleId)
        {
            var vehicle = await _vehicle.GetSingleAsync(x => x.VehicleId == vehicleId);

            return _mapper.Map<Vehicle.Domain.Entities.Vehicle, VehicleViewModel>(vehicle);
        }

        //GetVehicleName
        public async Task<VehicleViewModel> GetVehicleName(string vehicleName)
        {
            var vehicle = await _vehicle.GetSingleAsync(x => x.VehicleName == vehicleName);

            return _mapper.Map<Vehicle.Domain.Entities.Vehicle, VehicleViewModel>(vehicle);
        }

        public async Task<bool> ExistsAsync(string licenseNumber)
        {
            return await _vehicle.ExistsAsync(x => x.LicensePlateNumber == licenseNumber);
        }

        public async Task<VehicleViewModel> AddAsync(VehicleViewModel model)
        {
            var entity = new Vehicle.Domain.Entities.Vehicle
            {
                BodyType = model.BodyType,
                LastDateModified = DateTime.Now,
                Description = model.Description,
                Brand = model.Brand,
                Country = model.Country,
                Color = model.Color,
                LicensePlateNumber = model.LicensePlateNumber,
                Maker   = model.Maker,
                Model= model.Model,
                VehicleName = model.VehicleName,
                Year = model.Year
            };

            await _vehicle.AddAsync(entity);

            return _mapper.Map<Vehicle.Domain.Entities.Vehicle, VehicleViewModel>(entity);
        }

        public async Task UpdateAsync(VehicleViewModel model)
        {
            var entity = await _vehicle.GetSingleAsync(x => x.VehicleId == model.VehicleId);

            entity.LastDateModified = DateTime.Now;

            await _vehicle.UpdateAsync(entity);
        }

        public async Task<int> CountTotalVehiclesAsync()
        {
            return 1;
            // return await _clientAuthentication.CountAsync(x => x.AvailableFlag == true);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _vehicle.GetByIdAsync(id);

            await _vehicle.DeleteAsync(entity);
        }
    }
}
