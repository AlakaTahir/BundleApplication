using Bundle.Model.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bundle.Service.Interface
{
    public interface ITransactionHistoryService
    {
        Task<bool> CreateTransActionHistory(Guid userId, double amount, string transactionDescription, string typeofTransaction);
        Task<List<TransactionHistory>> GetTransactionHistoryByUserId(Guid userId);
        Task<List<TransactionHistory>> GetTodayCreditTransactionHistoryByUserId(Guid userId);

    }
}
