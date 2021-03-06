using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using VehicleTracking.Vehicle.Helper.Dto.Response;
using VehicleTracking.Vehicle.Helper.Notification;
using static VehicleTracking.Vehicle.Helper.Models.NotificationModel;

namespace VehicleTracking.Vehicles.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly INotificationVehicleTask _notification;

        protected BaseController(INotificationVehicleTask notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotifications;

        protected new ActionResult Response(BaseResponse response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            else
            {
                if (response == null)
                    response = new Response();

                response.Success = false;
                response.Errors = _notification.Notifications.Select(error => error);
                switch (_notification.Notifications.LastOrDefault().NotificationType)
                {
                    case ENotificationType.InternalServerError:
                        //TODO montar o log
                        //Log.Logger.BaseResponse(response);
                        return StatusCode((int)HttpStatusCode.InternalServerError, response);
                    case ENotificationType.BusinessRules:
                        return Conflict(response);
                    case ENotificationType.NotFound:
                        return NotFound(response);
                    default:
                        return BadRequest(response);
                }
            }
        }

        protected new IActionResult Response(object response = null)
        {

            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(new
                {
                    success = true,
                    data = response
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

        protected new IActionResult Response(int? id = null, object response = null)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

    }

    class Response : BaseResponse
    {
    }
}
