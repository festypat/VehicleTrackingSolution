using VehicleTracking.ApplicationCore.Vehicles.Common.Messaging;

namespace VehicleTracking.ApplicationCore.Vehicles.Events
{
    public class VehicleAddedEvent : IEvent
    {
        public string Id { get; }
        public string RegNr { get; }
        public string OwnerId { get; }

        public VehicleAddedEvent(string id, string regNr, string ownerId)
        {
            Id = id;
            RegNr = regNr;
            OwnerId = ownerId;
        }
    }
}
