using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Identity.Messaging;
using VehicleTracking.ApplicationCore.Identity.Services.Commands;

namespace VehicleTracking.ApplicationCore.Identity.Services.Handlers
{
    public class IdentityHandler : ICommandHandler<AddUserCommand>
    {
        public IdentityHandler()
        {

        }

        public async Task HandleAsync(AddUserCommand command)
        {
            //var owner = await _customerService.GetAsync(command.OwnerId)
            //                 .AnyContext();

            //if (owner == null)
            //{
            //    throw new AccException("customer_not_found", $"Customer: '{command.OwnerId}' was not found");
            //}

            //var vehicle = new Vehicle(command.Id,
            //            command.RegNr,
            //            command.Color,
            //            command.Brand,
            //            command.Model,
            //            command.Description,
            //            command.OwnerId,
            //            owner.Name);

            //await _vehicleRepository.AddAsync(vehicle)
            //                  .AnyContext();

            //var @event = new VehicleAddedEvent(vehicle.Id, vehicle.RegNr, vehicle.OwnerId);

            //await _busPublisher.PublishAsync(@event)
            //    .AnyContext();
        }

    }
}
