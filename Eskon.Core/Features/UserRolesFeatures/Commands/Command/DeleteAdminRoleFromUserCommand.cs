using Eskon.Core.Response;
using Eskon.Domian.DTOs.RolesDTOs;
using Eskon.Domian.DTOs.UserDTOs;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Command
{
    public record DeleteAdminRoleFromUserCommand(AdminRoleDTO AdminRole) : IRequest<Response<string>>;
}
