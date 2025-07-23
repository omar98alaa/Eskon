using Eskon.Core.Features.EscrowTransactionFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.Entities;
using Eskon.Service.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Queries.Handler
{
    public class EscrowTransactionQueryHandler : ResponseHandler
        , IRequestHandler<GetEscrowTransactionByBookingIdQuery, Response<EscrowTransaction?>>
        , IRequestHandler<GetAllPendingEscrowReleasesQuery, Response<List<EscrowTransaction>>>
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructors
        public EscrowTransactionQueryHandler(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        } 
        #endregion

        public async Task<Response<EscrowTransaction?>> Handle(GetEscrowTransactionByBookingIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _serviceUnitOfWork.EscrowTransactionService.GetByBookingIdAsync(request.BookingId);

            if (transaction == null)
                return NotFound<EscrowTransaction?>("Escrow transaction not found.");

            return Response<EscrowTransaction?>.Success(transaction);
        }

        public async Task<Response<List<EscrowTransaction>>> Handle(GetAllPendingEscrowReleasesQuery request, CancellationToken cancellationToken)
        {
            var pendingReleases = await _serviceUnitOfWork.EscrowTransactionService.GetAllPendingReleasesAsync();
            return Response<List<EscrowTransaction>>.Success(pendingReleases);
        }
    }
}
