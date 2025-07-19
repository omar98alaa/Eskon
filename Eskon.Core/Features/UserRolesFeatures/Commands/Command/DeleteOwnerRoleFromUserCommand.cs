using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Command
{
    public record DeleteOwnerRoleFromUserCommand(Guid UserToRemoveOwnerId) : IRequest<Response<TokenResponseDto>>;
}
