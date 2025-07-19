using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Eskon.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly ConcurrentDictionary<string, UserRefreshToken> _UserRefreshToken;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                     UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _UserRefreshToken = new ConcurrentDictionary<string, UserRefreshToken>();
        }


        #endregion

        #region Methods     

        public async Task<string> GenerateJWTTokenAsync(User user)
        {
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: isAdmin? DateTime.Now.AddMinutes(2) : DateTime.Now.AddMinutes(8),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }


        #endregion

    }
}
