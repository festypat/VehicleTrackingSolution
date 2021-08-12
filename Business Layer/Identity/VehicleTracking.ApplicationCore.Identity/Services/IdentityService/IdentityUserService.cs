using AutoMapper;
using Helper.Layer.Identity.Dto.Request;
using Helper.Layer.Identity.Dto.Response;
using Helper.Layer.Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using VehicleTracking.ApplicationCore.Identity.Jwt;
using VehicleTracking.ApplicationCore.Identity.Services.Interface;
using VehicleTracking.Identity.Domain.Entities;

namespace VehicleTracking.ApplicationCore.Identity.Services.IdentityService
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        public IdentityUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            TokenService tokenService)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateUserRequestDto, ApplicationUser>());

            _mapper = config.CreateMapper();
        }
        public async Task<ApiResponse> RegisterUser(CreateUserRequestDto model)
        {
            var response = new ApiResponse();
            try
            {
                var user = _mapper.Map<CreateUserRequestDto, ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    response.Data = result.Errors;

                    return response;
                }

                await _userManager.AddToRoleAsync(user, "Administrator");

                return response;
            }
            catch (Exception)
            {
                response.ResponseCode = "02";
                response.Message = "Internal Error";
                return response;
            }

        }

        public async Task<LoginResponse> AuthenticateUser(LoginRequestDto login)
        {
            var response = new LoginResponse();
            try
            {
                var user = await _userManager.FindByNameAsync(login.UserName);

                if (user != null)
                {
                    var signInResult = await _userManager.CheckPasswordAsync(user, login.Password);

                    if (signInResult)
                    {
                        response.ResponseCode = "00";
                        response.Message = "Success";

                        var roles = await _userManager.GetRolesAsync(user);

                        var userModel = new UserViewModel
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            Role = roles[0]
                        };

                        return await _tokenService.GenerateUserTokenAsync(userModel);
                    }
                    return response;
                }
                return response;
            }
            catch (Exception)
            {
                response.Message = "Internal error";
                response.ResponseCode = "03";

                return response;
            }
        }
    }
}
