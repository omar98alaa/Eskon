
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Commands.Command
{
    public record AddOwnerRoleToUserCommand(Guid UserToBeOwnerId) : IRequest<Response<TokenResponseDto>>;
}
