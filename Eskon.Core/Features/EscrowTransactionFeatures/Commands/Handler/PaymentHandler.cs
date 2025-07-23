
using AutoMapper;
using Eskon.Core.Features.EscrowTransactionFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Transaction;
using Eskon.Domian.Entities;
using Eskon.Service.Interfaces;
using Eskon.Service.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Commands.Handler
{
    public class PaymentHandler : ResponseHandler
        , IRequestHandler<CapturePaymentCommand, Response<bool>>
        , IRequestHandler<ReleaseToOwnerCommand, Response<bool>>
        , IRequestHandler<RefundToCustomerCommand, Response<bool>>
        , IRequestHandler<AddEscrowTransactionCommand, Response<EscrowTransaction>>
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PaymentHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<bool>> Handle(CapturePaymentCommand request, CancellationToken cancellationToken)
        {
            var success = await _serviceUnitOfWork.EscrowTransactionService.MarkPaymentCapturedAsync(request.CapturePaymentDto.BookingId, request.CapturePaymentDto.TransactionReference);

            if (!success)
                return Response<bool>.Fail("Escrow transaction not found or payment already captured");

            await _serviceUnitOfWork.SaveChangesAsync();
            return Response<bool>.Success(success, "Payment captured successfully");
        }

        public async Task<Response<bool>> Handle(ReleaseToOwnerCommand request, CancellationToken cancellationToken)
        {
            var success = await _serviceUnitOfWork.EscrowTransactionService
                .MarkReleasedToOwnerAsync(request.BookingId);

            if (!success)
                return Response<bool>.Fail("Could not release payment to the owner. Make sure payment is captured and not already released/refunded.");

            await _serviceUnitOfWork.SaveChangesAsync();
            return Response<bool>.Success(success, "Payment released to owner successfully.");
        }

        public async Task<Response<bool>> Handle(RefundToCustomerCommand request, CancellationToken cancellationToken)
        {
            var success = await _serviceUnitOfWork.EscrowTransactionService
                .MarkRefundedToCustomerAsync(request.BookingId);

            if (!success)
                return Response<bool>.Fail("Refund failed. Transaction might not be captured or already processed.");

            await _serviceUnitOfWork.SaveChangesAsync();
            return Response<bool>.Success(success, "Payment refunded to customer successfully.");
        }

        public async Task<Response<EscrowTransaction>> Handle(AddEscrowTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.EscrowTransactionDto);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.EscrowTransactionDto, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<EscrowTransaction>(internalErrorMessages);
            }
            var escrowTransaction = _mapper.Map<EscrowTransaction>(request.EscrowTransactionDto);

            var addedEscrowTransaction = await _serviceUnitOfWork.EscrowTransactionService.AddEscrowTransactionAsync(escrowTransaction);
            await _serviceUnitOfWork.SaveChangesAsync();

            return Success<EscrowTransaction>(addedEscrowTransaction, "Escrow transaction created successfully");
        }
    }
}
