using Bundle.Model.ViewModel;
using System;
using System.Threading.Tasks;

namespace Bundle.Service.Interface
{
    public interface IBundleInformationService
    {
        Task<BaseResponseViewModel> CreateUser(BundleInformationRequestModel model);
        Task<BaseResponseViewModel> UpdateUser(Guid id, BundleInformationRequestModel model);
        Task<BaseResponseViewModel> DeleteUser(Guid id);
        Task<BundleInformationResponseViewModel> GetUserById(Guid id);
        Task<string> RetrieveBalanceByPhoneNumber(string phonenumber);
        Task<string> AirtimeTopup(string phonenumber, double amount);
        Task<string> TransferAirtimeTopup(string phonenumber, double amount);
        Task<string> MakeACall(string initiatorPhoneNumber, double minutes);


    }
}
