using Eskon.Core.Response;
using Eskon.Domian.DTOs.EscrowTransaction;
using MediatR;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Commands.Command
{
    public record CapturePaymentCommand(CapturePaymentDto CapturePaymentDto) : IRequest<Response<bool>>; 
}
