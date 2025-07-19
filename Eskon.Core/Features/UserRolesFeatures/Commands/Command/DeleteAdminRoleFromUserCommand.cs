using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Command
{
    public record DeleteAdminRoleFromUserCommand(Guid UserToRemoveAdminId) : IRequest<Response<string>>;
}
