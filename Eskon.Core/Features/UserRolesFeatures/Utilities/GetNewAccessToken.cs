using Eskon.Core.Response;
using Eskon.Domian.DTOs.RefreshTokenDTOs;
using Eskon.Domian.DTOs.UserDTOs;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Utilities
{
    public record GetNewAccessToken(CurrentRefreshTokenDTO RefreshToken) : IRequest<Response<TokenResponseDto>>;
}
