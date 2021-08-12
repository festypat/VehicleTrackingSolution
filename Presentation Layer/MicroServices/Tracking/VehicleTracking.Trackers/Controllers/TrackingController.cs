using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Trackers.BusService;
using VehicleTracking.Tracking.Helper.Dto.Request;
using VehicleTracking.Tracking.Helper.Notification;

namespace VehicleTracking.Trackers.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [Route("api/location")]
    [ApiController]
    public class TrackingController : BaseController
    {
        private readonly ISendMessageService _sendMessageService;
        public TrackingController(ISendMessageService sendMessageService, INotificationTask notification) : base(notification)
        {
            _sendMessageService = sendMessageService ?? throw new ArgumentNullException(nameof(sendMessageService));
        }

        [HttpPost]
        [Route("create-location")]
        public async Task<IActionResult> AddLocation([FromBody] CreateLocationDto request) => Response(await _sendMessageService.ProcessAsync(request).ConfigureAwait(false));

        [HttpGet]
        [Route("get-location")]
        public async Task<IActionResult> GetLocation([FromQuery] string locationId) => Response(await _sendMessageService.GetLocationAsync(locationId).ConfigureAwait(false));

        [HttpGet]
        [Route("get-specific-location")]
        public async Task<IActionResult> GetSpecificLocation([FromQuery] LocationRequestDto request) => Response(await _sendMessageService.GetSpecificLocationAsync(request).ConfigureAwait(false));
    }
}
