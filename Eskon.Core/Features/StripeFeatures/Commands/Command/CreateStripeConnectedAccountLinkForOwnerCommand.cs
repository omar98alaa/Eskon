using Eskon.Core.Response;
using Eskon.Domian.DTOs.StripeDTOs;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Command
{
    public record CreateStripeConnectedAccountLinkForOwnerCommand(CreateStripeConnectedAccountFillLinkRequestDTO RequestDTO) : IRequest<Response<string>>;
}
