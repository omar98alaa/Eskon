using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eskon.Service.Services
{
    public class TransactionService : ITransactionService
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;
        #endregion

        #region Constructors
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        #endregion

        #region Methods
        public async Task<List<Transaction>> GetAllForUserAsync(Guid userId)
        {
            return await _transactionRepository.GetAllTransactionsForUserAsync(userId);
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime Startdate, DateTime EndDate)
        {
            return await _transactionRepository.GetTransactionsByDateRangeAsync(Startdate, EndDate);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await _transactionRepository.AddAsync(transaction);
        }
        #endregion
    }
}