using Arch.EntityFrameworkCore.UnitOfWork;
using Bundle.Model.Entity;
using Bundle.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bundle.Service.Service
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateTransActionHistory(Guid userId,double amount,string transactionDescription,string typeofTransaction) 
     {
            var newTransactionHistory = new TransactionHistory();
            newTransactionHistory.Id = Guid.NewGuid();
            newTransactionHistory.UserId = userId;
            newTransactionHistory.Date = DateTime.Now;
            newTransactionHistory.Amount = amount;
            newTransactionHistory.TransactionDescription = transactionDescription;
            newTransactionHistory.TypeOfTransaction = typeofTransaction;

            _unitOfWork.GetRepository<TransactionHistory>().Insert(newTransactionHistory);
             await _unitOfWork.SaveChangesAsync();
             
            return true;
     }
        public async Task<List<TransactionHistory>> GetTransactionHistoryByUserId(Guid userId) 
        {
         var transactionHistory = _unitOfWork.GetRepository<TransactionHistory>().GetAll().Where(t => t.UserId == userId).ToList();
            
            return transactionHistory;
        }

        public async Task<List<TransactionHistory>> GetTodayCreditTransactionHistoryByUserId(Guid userId) 
        {
            var transactionHistory = _unitOfWork.GetRepository<TransactionHistory>().GetAll().Where(t => t.UserId == userId && t.TypeOfTransaction == "Credit" && t.Date.Year == DateTime.Now.Year && t.Date.Month == DateTime.Now.Month && t.Date.Day == DateTime.Now.Day).ToList();
            
                return transactionHistory;
        }

    }
}
