
using Eskon.Domian.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eskon.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
        public Task<(JwtSecurityToken, string)> GenerateJWTToken(User user);
        public Task<List<Claim>> GetClaims(User user);

    }
}
