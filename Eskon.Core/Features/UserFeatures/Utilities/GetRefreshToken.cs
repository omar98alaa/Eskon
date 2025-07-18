using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Utilities
{
    public record GetRefreshToken(string token) : IRequest<Response<TokenResponseDto>>;
}
