using Eskon.Core.Response;
using Eskon.Domian.DTOs.RefreshToken;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Utilities
{
    public record GetNewAccessToken(CurrentRefreshTokenDTO RefreshToken) : IRequest<Response<TokenResponseDto>>;
}
