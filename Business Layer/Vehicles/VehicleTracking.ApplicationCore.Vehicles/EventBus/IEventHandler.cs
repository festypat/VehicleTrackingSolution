using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.Events;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus
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
