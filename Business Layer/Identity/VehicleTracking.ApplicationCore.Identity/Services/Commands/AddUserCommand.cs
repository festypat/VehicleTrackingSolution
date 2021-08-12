using System.Text.Json.Serialization;
using VehicleTracking.ApplicationCore.Identity.Messaging;

namespace VehicleTracking.ApplicationCore.Identity.Services.Commands
{
    public class AddUserCommand : ICommand
    {
        public string Id { get; }
        public string RegNr { get; }
        public string Brand { get; }
        public string Color { get; }
        public string Model { get; }
        public string Description { get; }
        public string OwnerId { get; }

        [JsonConstructor]
        public AddUserCommand(string id,
            string regNr,
           string brand,
           string color,
           string model,
           string description,
           string ownerId)
        {
            Id = id;
            RegNr = regNr;
            Brand = brand;
            Color = color;
            Model = model;
            Description = description;
            OwnerId = ownerId;
        }
    }

}
