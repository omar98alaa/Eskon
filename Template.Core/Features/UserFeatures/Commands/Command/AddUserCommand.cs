using MediatR;
using Template.Core.Features.UserFeatures.Commands;
using Template.Core.Response;
using Template.Domian.DTOs.User;
using Template.Domian.Entities.Identity;

namespace Template.Core.Features.UserFeatures.Commands
{
    public record AddUserCommand(User User) : IRequest<Response<UserReadDto>>;
}
