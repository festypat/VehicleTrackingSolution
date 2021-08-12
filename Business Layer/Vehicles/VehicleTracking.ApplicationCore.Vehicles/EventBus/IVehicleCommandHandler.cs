using MediatR;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus
{
    public interface IVehicleCommandHandler<T, R> : IRequestHandler<T, R> where T : IRequest<R>
    {

    }
}
