using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Commands.Command
{
    public record DeleteAdminRoleFromUserCommand(Guid UserToRemoveAdminId) : IRequest<Response<TokenResponseDto>>;
}
