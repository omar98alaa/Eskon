﻿using MediatR;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Core.Features.UserFeatures.Commands
{
    public record AddUserCommand(User User) : IRequest<Response<UserReadDto>>;
}
