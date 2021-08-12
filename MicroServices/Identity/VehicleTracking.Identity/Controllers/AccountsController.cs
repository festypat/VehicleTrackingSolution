using Helper.Layer.Identity.Dto.Request;
using Helper.Layer.Identity.Notification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Identity.Services;

namespace VehicleTracking.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [Route("api/account")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly IdentityRepository _identityRepository;

        public AccountsController(IdentityRepository identityRepository, INotificationIdentityTask notification) : base(notification)
        {
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }
    

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegsiterUser([FromBody] CreateUserRequestDto request) => Response(await _identityRepository.CreateUserAsync(request).ConfigureAwait(false));

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequestDto request) => Response(await _identityRepository.LoginUserAsync(request).ConfigureAwait(false));

    }
}
