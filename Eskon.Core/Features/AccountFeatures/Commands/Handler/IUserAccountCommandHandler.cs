using Eskon.Core.Features.AccountFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.UserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.AccountFeatures.Commands.Handler
{
    public interface IUserAccountCommandHandler : IRequestHandler<AddUserAccountCommand, Response<UserReadDto>>, 
                                                  IRequestHandler<SignInUserCommand, Response<TokenResponseDto>>,
                                                  IRequestHandler<SignOutUserCommand, Response<string>>;
}
