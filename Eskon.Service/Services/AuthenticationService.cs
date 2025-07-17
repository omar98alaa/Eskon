using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eskon.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        #endregion 

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                     UserManager<User> userManager,
                                     MyDbContext myDbContext)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }


        #endregion

        #region
        public async Task<string> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);

            return accessToken;
        }

        public async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim("Username",user.UserName),
                new Claim("Email",user.Email),
                new Claim("UserId", user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim("Roles", role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }
        #endregion

    }
}
