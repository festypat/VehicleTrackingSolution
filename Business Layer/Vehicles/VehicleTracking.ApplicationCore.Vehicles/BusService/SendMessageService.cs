using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.Commands;
using VehicleTracking.Vehicle.Helper.Dto.Request;
using VehicleTracking.ApplicationCore.Vehicles.EventBus;

namespace VehicleTracking.ApplicationCore.Vehicles.BusService
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IEventBus _bus;
        public SendMessageService(IEventBus bus)
        {
            _bus = bus;
        }

        public async Task<string> ProcessAsync(CreateVehicleDto message)
        {
            await SendMessageAsync(message);
            return "true";
        }

        public async Task SendMessageAsync(CreateVehicleDto message)
        {
            var createTransferCommand = new CreateNewVehicleCommand(
               message.VehicleName,
               message.Brand,
               message.Color,
               message.Description,
               message.Model,
               message.Maker,
               message.BodyType,
               message.LicensePlateNumber,
               message.Year,
               message.Country
           );

           await _bus.SendCommand(createTransferCommand);
        }
    }
}
