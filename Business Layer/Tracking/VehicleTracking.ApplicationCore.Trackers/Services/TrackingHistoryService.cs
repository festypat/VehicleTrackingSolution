using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Repositories;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.Tracking.Domain.Entities;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.Services
{

    public class TrackingHistoryService : ITrackingHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<TrackingHistory> _trackingHistory;

        public TrackingHistoryService(IAsyncRepository<TrackingHistory> trackingHistory)
        {
            _trackingHistory = trackingHistory;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TrackingHistory, TrackingViewModel>());

            _mapper = config.CreateMapper();
        }

        public async Task<List<TrackingViewModel>> GetAllAsync()
        {
            var histories = await _trackingHistory.GetAllAsync();

            return _mapper.Map<List<TrackingHistory>, List<TrackingViewModel>>(histories);
        }

        public async Task<List<TrackingViewModel>> GetAllHistoryByDateAsync(LocationRequestDto model)
        {
            DateTime sDate = DateTime.ParseExact(model.StartDate, "dd-MMM-yyyy",
            CultureInfo.InvariantCulture);

            DateTime eDate = DateTime.ParseExact(model.EndDate, "dd-MMM-yyyy",
            CultureInfo.InvariantCulture);
            //(n => n.AddDate.Date >= stdate.Date &&
            // n.AddDate.Date <= etdate

            var histories = await _trackingHistory
                .GetAsync(x => x.DeviceId == model.DeviceId
                && x.DateEntered.Date >= sDate.Date
                && x.DateEntered.Date <= eDate.Date
                );

            //var histories = await _trackingHistory
            //    .GetAsync(x => x.DeviceId == model.DeviceId 
            //    && x.DateEntered.Year == sDate.Year
            //    && x.DateEntered.Month == sDate.Month
            //    && x.DateEntered.Day == sDate.Day

            //    && x.DateEntered >

            //    );

            return _mapper.Map<List<TrackingHistory>, List<TrackingViewModel>>(histories);
        }

        //GetAllHistoryByDateAsync
        public async Task<TrackingViewModel> GetHistoryInfo(long historyId)
        {
            var history = await _trackingHistory.GetSingleAsync(x => x.TrackingHistoryId == historyId);

            return _mapper.Map<TrackingHistory, TrackingViewModel>(history);
        }

        public async Task<bool> ExistsAsync(long Id)
        {
            return await _trackingHistory.ExistsAsync(x => x.TrackingHistoryId == Id);
        }

        public async Task<TrackingViewModel> AddAsync(TrackingViewModel model)
        {
            var entity = new TrackingHistory
            {
                Latitude = model.Latitude,
                VehicleId = model.VehicleId,
                Longitude = model.Longitude,
                DeviceId = model.DeviceId,
            };

            await _trackingHistory.AddAsync(entity);

            return _mapper.Map<TrackingHistory, TrackingViewModel>(entity);
        }
    }
}
