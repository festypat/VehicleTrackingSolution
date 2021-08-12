using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.Commands;
using VehicleTracking.ApplicationCore.Vehicles.Interfaces.Service;
using VehicleTracking.ApplicationCore.Vehicles.EventBus;
using MediatR;
using VehicleTracking.Vehicle.Helper.Exceptions;
using VehicleTracking.Vehicle.Helper.ViewModel;
using AutoMapper;

namespace VehicleTracking.ApplicationCore.Vehicles.Handlers
{
    public class VehicleHandler : IRequestHandler<CreateNewVehicleCommand, bool>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IEventBus _bus;
        private readonly IMapper _mapper;
        public VehicleHandler(IVehicleService vehicleService, IEventBus bus)
        {
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateNewVehicleCommand, VehicleViewModel>());

            _mapper = config.CreateMapper();
        }

        public async Task<bool> Handle(CreateNewVehicleCommand request, CancellationToken cancellationToken)
        {

            var vehicle = await _vehicleService.ExistsAsync(request.LicensePlateNumber);

            if (vehicle)
                throw new AccException("409", $"Customer: '{request.VehicleName}' was not found");

            await _vehicleService.AddAsync(_mapper.Map<CreateNewVehicleCommand, VehicleViewModel>(request));

            return true;
        }

        //public async Task HandleAsync(AddVehicleCommand command, CancellationToken cancellationToken)
        //{
        //    var owner = await _customerService.GetAsync(command.OwnerId)
        //                     .AnyContext();

        //    if (owner == null)
        //    {
        //        throw new AccException("customer_not_found", $"Customer: '{command.OwnerId}' was not found");
        //    }

        //    var vehicle = new Vehicle(command.Id,
        //                command.RegNr,
        //                command.Color,
        //                command.Brand,
        //                command.Model,
        //                command.Description,
        //                command.OwnerId,
        //                owner.Name);

        //    await _vehicleRepository.AddAsync(vehicle)
        //                      .AnyContext();

        //    var @event = new VehicleAddedEvent(vehicle.Id, vehicle.RegNr, vehicle.OwnerId);

        //    await _commandBus.PublishAsync(new AddVehicleCommand(command.Id, command.RegNr, 
        //        command.Brand, command.Color, command.Model, 
        //        command.Description, command.OwnerId, @event),
        //           cancellationToken);
        //}

    }

}
