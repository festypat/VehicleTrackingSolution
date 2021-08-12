using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.Events;

namespace VehicleTracking.ApplicationCore.Trackers.EventBus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}
