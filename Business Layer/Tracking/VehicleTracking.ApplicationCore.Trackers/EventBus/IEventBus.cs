using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Commands;
using VehicleTracking.ApplicationCore.Trackers.Events;

namespace VehicleTracking.ApplicationCore.Trackers.EventBus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
