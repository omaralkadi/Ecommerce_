using AmazonCore.Entities.Identity;
using AmazonCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configration;

        public TokenService(IConfiguration configration)
        {
            _configration = configration;
        }
        public async Task<string> CreateToken(AppUser user)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email) 
            };

            var ApiKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configration["JWT:key"]));

            var signingCredentials = new SigningCredentials(ApiKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configration["JWT:Issuer"],
                audience: _configration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configration["JWT:DurationInDays"])), // Set token expiration (e.g., 1 day)
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
