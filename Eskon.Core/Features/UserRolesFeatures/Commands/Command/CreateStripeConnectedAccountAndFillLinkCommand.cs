
using Eskon.Core.Response;
using Eskon.Domian.DTOs.RolesDTOs;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Command
{
    public record CreateStripeConnectedAccountAndFillLinkCommand(Guid UserToBeOwnerId, OwnerRoleDTO OwnerRoleDTO) : IRequest<Response<string>>;
}