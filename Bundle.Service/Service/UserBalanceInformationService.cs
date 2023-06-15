using Arch.EntityFrameworkCore.UnitOfWork;
using Bundle.Model.Entity;
using Bundle.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Bundle.Service.Service
{
    public class UserBalanceInformationService : IUserBalanceInformationService
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserBalanceInformationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //create a user balance on creating user
        public async Task<bool> InitiateBalance(Guid userId)
        {
            var newUserBalance = new UserBalance();
            newUserBalance.Id = Guid.NewGuid();
            newUserBalance.UserId = userId;
            newUserBalance.Balance = 0;
            newUserBalance.CreatedDate = DateTime.Now;

            _unitOfWork.GetRepository<UserBalance>().Insert(newUserBalance);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TopUpBalance(Guid userId, double amount) 
        {
            var userBalance = _unitOfWork.GetRepository<UserBalance>().GetFirstOrDefault(predicate: y => y.UserId == userId);
            if (userBalance != null) 
            {
                userBalance.Balance = userBalance.Balance + amount;
                userBalance.UpadatedDate = DateTime.Now;

                _unitOfWork.GetRepository<UserBalance>().Update(userBalance);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            else
            { 
                return false;
            
            }
            
            

        } 
        
        public async Task<bool> ChargingCustomer(Guid userId, double amount) 
        {
                            
          var userBalance = _unitOfWork.GetRepository<UserBalance>().GetFirstOrDefault(predicate: x => x.UserId == userId);
            if (userBalance != null)
            {
                userBalance.Balance = userBalance.Balance - amount;
                userBalance.UpadatedDate = DateTime.Now;

                _unitOfWork.GetRepository<UserBalance>().Update(userBalance);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;

            }


        }
        public async Task<double> RetrieveUserBalance(Guid userId) 
        {
            var userBalance = _unitOfWork.GetRepository<UserBalance>().GetFirstOrDefault(predicate: x => x.UserId == userId);
            if (userBalance != null) 
            {
                return userBalance.Balance;
               
            }
            else
            {
                return 0;
            }


        }


    }  
        
        
        
    
}// update balance after every recharge
//topup balance
//charging customer


//retrive user balance
   