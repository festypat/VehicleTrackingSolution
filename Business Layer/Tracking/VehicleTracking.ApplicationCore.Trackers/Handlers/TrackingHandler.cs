using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Commands;
using VehicleTracking.ApplicationCore.Trackers.EventBus;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.Tracking.Helper.Extensions;
using VehicleTracking.Tracking.Helper.ViewModel;

namespace VehicleTracking.ApplicationCore.Trackers.Handlers
{
    public class TrackingHandler : IRequestHandler<CreateNewLocationCommand, bool>
    {
        private readonly ITrackingService _trackingService;
        private readonly IEventBus _bus;
        private readonly IMapper _mapper;
        public TrackingHandler(ITrackingService trackingService, IEventBus bus)
        {
            _trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateNewLocationCommand, TrackingViewModel>());

            _mapper = config.CreateMapper();
        }
        public async Task<bool> Handle(CreateNewLocationCommand request, CancellationToken cancellationToken)
        {

            var location = await _trackingService.ExistsAsync(request.DeviceId);

            if (location)
                throw new AccException("409", $"Customer: '{request.DeviceId}' was not found");

            await _trackingService.AddAsync(_mapper.Map<CreateNewLocationCommand, TrackingViewModel>(request));

            return true;
        }

    }
}
