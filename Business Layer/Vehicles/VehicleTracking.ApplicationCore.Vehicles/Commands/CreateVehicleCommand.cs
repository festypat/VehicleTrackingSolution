using MediatR;
using System.Text.Json.Serialization;
using VehicleTracking.Vehicle.Helper.Dto.Request;

namespace VehicleTracking.ApplicationCore.Vehicles.Commands
{
    public class CreateVehicleCommand : IRequest<CreateVehicleDto>
    {
        public string Id { get; set; }
        public string RegNr { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }

        public CreateVehicleCommand() { }       

        [JsonConstructor]
        public CreateVehicleCommand(string id,
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
