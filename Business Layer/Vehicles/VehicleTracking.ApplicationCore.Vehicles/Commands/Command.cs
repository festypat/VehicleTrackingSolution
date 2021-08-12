using System;
using VehicleTracking.ApplicationCore.Vehicles.Events;

namespace VehicleTracking.ApplicationCore.Vehicles.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

    }
}
