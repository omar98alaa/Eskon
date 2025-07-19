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
        private readonly ConcurrentDictionary<string, UserRefreshToken> _UserRefreshToken;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _UserRefreshToken = new ConcurrentDictionary<string, UserRefreshToken>();
        }
        #endregion

        #region Methods     
        public string GenerateJWTTokenAsync(User user ,IList<string> userManagerRoles, IList<Claim> userManagerClaims)
        {
            var userAllClaims = GeneratedAllUserClaims(user, userManagerRoles, userManagerClaims);
            bool isAdmin = false;
            foreach (var role in userManagerRoles)
            {
                if(role == "Admin")
                {
                    isAdmin = true;
                }
            }

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                userAllClaims,
                expires: isAdmin ? DateTime.Now.AddMinutes(2) : DateTime.Now.AddMinutes(8),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        private List<Claim> GeneratedAllUserClaims(User user, IList<string> userManagerRoles, IList<Claim> userManagerClaims)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in userManagerRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userManagerClaims);
            return claims;
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        #endregion

    }
}
