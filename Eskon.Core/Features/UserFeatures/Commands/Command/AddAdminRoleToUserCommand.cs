
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Commands.Command
{
    public record AddAdminRoleToUserCommand(Guid UserToBeAdminId) : IRequest<Response<TokenResponseDto>>;
}
