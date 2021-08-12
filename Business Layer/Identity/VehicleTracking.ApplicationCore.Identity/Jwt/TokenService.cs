using Helper.Layer.Identity.Dto.Response;
using Helper.Layer.Identity.Extensions.Utilities;
using Helper.Layer.Identity.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.ApplicationCore.Identity.Jwt
{
    public class TokenService
    {
        private readonly IdentityConfiguration _identityConfiguration;
        public TokenService(IOptions<IdentityConfiguration> identityConfiguration)
        {
            _identityConfiguration = identityConfiguration.Value;
        }

        public async Task<LoginResponse> GenerateUserTokenAsync(UserViewModel userViewModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_identityConfiguration.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userViewModel.UserName),
                    new Claim(ClaimTypes.Role, userViewModel.Role),
                    new Claim(ClaimTypes.Email, userViewModel.Email),
                }),

                Expires = DateTime.UtcNow.AddDays(_identityConfiguration.TokenExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenResult = new TokenResponse();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            tokenResult.AccessToken = tokenString;
            tokenResult.Role = userViewModel.Role;
            tokenResult.UserName = userViewModel.UserName;

            return await Task.FromResult(new LoginResponse
            {
                ResponseCode = "00",
                tokenResponse = tokenResult,
                Message = "Success"
            });

        }
    }
}
