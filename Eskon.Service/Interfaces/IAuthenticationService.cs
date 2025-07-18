
using Eskon.Domian.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eskon.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GenerateJWTTokenAsync(User user);
        //public Task<JwtAuthResult> GenerateJWTToken(User user);
        public Task<List<Claim>> GetClaims(User user);

        public string GenerateRefreshToken();

    }
}
