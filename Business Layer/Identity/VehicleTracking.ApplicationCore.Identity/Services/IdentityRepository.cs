using Helper.Layer.Identity.Dto.Request;
using Helper.Layer.Identity.Dto.Response;
using System;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Identity.Services.Interface;

namespace VehicleTracking.ApplicationCore.Identity.Services
{
    public class IdentityRepository
    {
        private readonly IIdentityUserService _identityUserService;
        public IdentityRepository(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService ?? throw new ArgumentNullException(nameof(identityUserService));
        }
        public async Task<ApiResponse> CreateUserAsync(CreateUserRequestDto model)
        {
            return await _identityUserService.RegisterUser(model);
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequestDto model)
        {
            return await _identityUserService.AuthenticateUser(model);
        }
    }
}
