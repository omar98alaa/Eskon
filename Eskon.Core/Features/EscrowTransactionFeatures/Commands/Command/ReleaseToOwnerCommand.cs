using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Commands.Command
{
    public record ReleaseToOwnerCommand(Guid BookingId) : IRequest<Response<bool>>;

}
