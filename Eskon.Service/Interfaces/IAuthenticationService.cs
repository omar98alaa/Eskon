using Eskon.Domian.Entities.Identity;
using System.Security.Claims;

namespace Eskon.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public string GenerateJWTTokenAsync(User user, IList<string> userManagerRoles, IList<Claim> userManagerClaims);
        public string GenerateRefreshToken();

    }
}
