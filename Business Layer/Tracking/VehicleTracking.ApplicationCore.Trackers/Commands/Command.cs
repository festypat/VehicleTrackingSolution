using System;
using VehicleTracking.ApplicationCore.Trackers.Events;

namespace VehicleTracking.ApplicationCore.Trackers.Commands
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
