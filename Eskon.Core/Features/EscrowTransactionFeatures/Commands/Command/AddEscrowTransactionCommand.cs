using Eskon.Core.Response;
using Eskon.Domian.DTOs.EscrowTransaction;
using Eskon.Domian.Entities;
using MediatR;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Commands.Command
{
    public record AddEscrowTransactionCommand(AddEscrowTransactionDTO EscrowTransactionDto)
        : IRequest<Response<EscrowTransaction>>;
}
