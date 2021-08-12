using Helper.Layer.Identity.Dto.Request;
using Helper.Layer.Identity.Dto.Response;
using System.Threading.Tasks;

namespace VehicleTracking.ApplicationCore.Identity.Services.Interface
{
    public interface IIdentityUserService
    {
        Task<ApiResponse> RegisterUser(CreateUserRequestDto createUser);
        Task<LoginResponse> AuthenticateUser(LoginRequestDto login);
    }
}
