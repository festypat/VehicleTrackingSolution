using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Repositories;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.Tracking.Domain.Entities;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.Services
{

    public class TrackingService : ITrackingService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Location> _location;

        public TrackingService(IAsyncRepository<Location> location)
        {
            _location = location;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Location, TrackingViewModel>());

            _mapper = config.CreateMapper();
        }

        public async Task<List<TrackingViewModel>> GetAllAsync()
        {
            var locations = await _location.GetAllAsync();

            return _mapper.Map<List<Location>, List<TrackingViewModel>>(locations);
        }

        public async Task<TrackingViewModel> GetTrackingInfo(long locationId)
        {
            var location = await _location.GetSingleAsync(x => x.LocationId == locationId);

            return _mapper.Map<Location, TrackingViewModel>(location);
        }

        public async Task<bool> ExistsAsync(long Id)
        {
            return await _location.ExistsAsync(x => x.LocationId == Id);
        }

        public async Task<TrackingViewModel> AddAsync(TrackingViewModel model)
        {
            var entity = new Location
            {
                Latitude = model.Latitude,
                VehicleId = model.VehicleId,
                Longitude = model.Longitude,
                DeviceId = model.DeviceId,
                LastDateModified = DateTime.Now
            };

            await _location.AddAsync(entity);

            return _mapper.Map<Location, TrackingViewModel>(entity);
        }

        public async Task UpdateAsync(TrackingViewModel model)
        {
            var entity = await _location.GetSingleAsync(x => x.DeviceId == model.DeviceId);

            entity.LastDateModified = DateTime.Now;
            entity.Latitude = model.Latitude;
            entity.Longitude = model.Longitude;
            entity.DisplayName = model.DisplayName;
            entity.PlaceId = model.PlaceId;
            entity.Licence = model.Licence;

            await _location.UpdateAsync(entity);
        }

        public async Task<int> CountTotalTrackingAsync()
        {
            return 1;
            // return await _clientAuthentication.CountAsync(x => x.AvailableFlag == true);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _location.GetByIdAsync(id);

            await _location.DeleteAsync(entity);
        }
    }

}
