

using Eskon.Core.Response;
using Eskon.Domain.DTOs.Transaction;
using Eskon.Domian.DTOs.Transaction;
using MediatR;

namespace Eskon.Core.Features.TransactionFeatures.Commands.Command
{
    public record AddTransactionCommand(TransactionInputDTO transactionInputDTO) : IRequest<Response<TransactionReadDTO>>;

}
