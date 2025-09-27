using Eskon.Core.Response;
using Eskon.Domian.DTOs.UserDTOs;
using Eskon.Domian.Entities.Identity;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Command
{
    public record AddOwnerRoleToUserCommand(User UserToBeOwner) : IRequest<Response<string>>;
}
