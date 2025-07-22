using AutoMapper;
using Eskon.Core.Features.TransactionFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Transaction;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.TransactionFeatures.Commands.Handler
{
    public class TransactionCommandHandler : ResponseHandler, IRequestHandler<AddTransactionCommand, Response<TransactionReadDTO>>
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public TransactionCommandHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        public async Task<Response<TransactionReadDTO>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.transactionInputDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.transactionInputDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<TransactionReadDTO>(internalErrorMessages);
            }

            var Sender = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.transactionInputDTO.SenderId);
            var Reciever = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.transactionInputDTO.RecieverId);

            if (Sender == null || Reciever == null) { return NotFound<TransactionReadDTO>("Sender or Reciever not found"); }
            if (Sender.WalletAmount < request.transactionInputDTO?.Amount) { return BadRequest<TransactionReadDTO>("Insufficient Balance"); }
            
            Sender.WalletAmount   -= request.transactionInputDTO.Amount;
            Reciever.WalletAmount += request.transactionInputDTO.Amount;

            Transaction addedTransaction = _mapper.Map<Transaction>(request.transactionInputDTO);
            addedTransaction.Fee = _serviceUnitOfWork.TransactionService.CalculateSystemFeesForTransaction(request.transactionInputDTO.Amount);
            await _serviceUnitOfWork.TransactionService.CreateTransactionAsync(addedTransaction);
            await _serviceUnitOfWork.SaveChangesAsync();
            TransactionReadDTO addedTransactionReadDTO = _mapper.Map<TransactionReadDTO>(addedTransaction);
            return Created(addedTransactionReadDTO);
        }
        #endregion
    }
}
