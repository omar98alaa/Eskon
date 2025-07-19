using Eskon.Domian.Entities.Identity;
using System.Security.Claims;

namespace Eskon.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> GenerateJWTTokenAsync(User user);

        public Task<List<Claim>> GetClaims(User user);

        public string GenerateRefreshToken();

    }
}
