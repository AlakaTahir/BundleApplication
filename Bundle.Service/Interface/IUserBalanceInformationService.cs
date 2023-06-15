using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bundle.Service.Interface
{
    public interface IUserBalanceInformationService
    {
        Task<bool> InitiateBalance(Guid userId);
        Task<bool> TopUpBalance(Guid userId, double amount);
        Task<bool> ChargingCustomer(Guid userId, double amount);
        Task<double> RetrieveUserBalance(Guid userId);
    }
}
