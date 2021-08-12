using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Vehicles.BusService;
using VehicleTracking.Vehicle.Helper.Dto.Request;
using VehicleTracking.Vehicle.Helper.Notification;

namespace VehicleTracking.Vehicles.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : BaseController
    {
        private readonly ISendMessageService _sendMessageService;

        public VehiclesController(ISendMessageService sendMessageService, INotificationVehicleTask notification) : base(notification)
        {
            _sendMessageService = sendMessageService ?? throw new ArgumentNullException(nameof(sendMessageService));
        }

        [HttpPost]
        [Route("create-new-vehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto request) => Response(await _sendMessageService.ProcessAsync(request).ConfigureAwait(false));

    }
}
