using System.Threading.Tasks;

namespace VehicleTracking.ApplicationCore.Identity.Messaging
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
