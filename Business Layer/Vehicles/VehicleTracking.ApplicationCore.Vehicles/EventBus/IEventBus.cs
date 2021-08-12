using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.Commands;
using VehicleTracking.ApplicationCore.Vehicles.Events;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }

}
