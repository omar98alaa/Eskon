using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Commands.Handler
{
    public interface IUserRolesCommandHandler : 
          IRequestHandler<AddOwnerRoleToUserCommand, Response<TokenResponseDto>>
        , IRequestHandler<AddAdminRoleToUserCommand, Response<string>>
        , IRequestHandler<DeleteAdminRoleFromUserCommand, Response<string>>;
}
