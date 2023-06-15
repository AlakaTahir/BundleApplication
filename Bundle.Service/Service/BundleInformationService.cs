using Arch.EntityFrameworkCore.UnitOfWork;
using Bundle.Model.Entity;
using Bundle.Model.ViewModel;
using Bundle.Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bundle.Service.Service
{
    public class BundleInformationService : IBundleInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserBalanceInformationService _userBalanceInformationService;
        private readonly ITransactionHistoryService _transactionHistoryService;

        public BundleInformationService(IUnitOfWork unitOfWork, IUserBalanceInformationService userBalanceInformationService, ITransactionHistoryService transactionHistoryService)
        {
            _unitOfWork = unitOfWork;
            _userBalanceInformationService = userBalanceInformationService;
            _transactionHistoryService = transactionHistoryService;
        }

        public async Task<BaseResponseViewModel> CreateUser(BundleInformationRequestModel model)

        {
            //check if user exist
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y => y.Email == model.Email);
            //if user does not exist
            if (user == null)
            {
                var newUser = new UserInfo();
                newUser.Id = Guid.NewGuid();
                newUser.Email = model.Email;
                newUser.Name = model.Name;
                newUser.PhoneNumber = model.PhoneNumber;
                newUser.Createddate = DateTime.Now;

                _unitOfWork.GetRepository<UserInfo>().Insert(newUser);
                await _unitOfWork.SaveChangesAsync();

                //on user created, initiate balance
                await _userBalanceInformationService.InitiateBalance(newUser.Id);

                return new BaseResponseViewModel
                {
                    Message = "Created Successfully",
                    Status = true
                };


            }
            return new BaseResponseViewModel
            {
                Message = "User Already Exist",
                Status = false
            };
        }
        public async Task<BaseResponseViewModel> UpdateUser(Guid id, BundleInformationRequestModel model)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y => y.Id == id);
            if (user != null)
            {
                user.Email = model.Email;
                user.Name = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                user.UpdatedDate = DateTime.Now;

                _unitOfWork.GetRepository<UserInfo>().Update(user);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseViewModel
                {
                    Message = "Updated Successfully",
                    Status = true
                };
            }
            else
                return new BaseResponseViewModel
                {
                    Message = "Unsuccessful",
                    Status = false
                };

        }
        public async Task<BaseResponseViewModel> DeleteUser(Guid id)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y => y.Id == id);
            if (user != null)
            {
                _unitOfWork.GetRepository<UserInfo>().Delete(user);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseViewModel
                {
                    Message = "Deleted Successfully",
                    Status = true
                };
            }
            else
                return new BaseResponseViewModel
                {
                    Message = "Not Found",
                    Status = false
                };
        }
        public async Task<BundleInformationResponseViewModel> GetUserById(Guid id)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y => y.Id == id);
            if (user != null)
            {
                return new BundleInformationResponseViewModel
                {

                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                };
            }
            else
            {
                return null;
            }
        }
        public async Task<string> RetrieveBalanceByPhoneNumber(string phonenumber)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: x => x.PhoneNumber == phonenumber);
            if (user != null)
            {
                var userbalance = await _userBalanceInformationService.RetrieveUserBalance(user.Id);
                return "NGN VTU" + $"{userbalance}";
            }
            return "0";
        }

        public async Task<string> AirtimeTopup(string phonenumber, double amount)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: x => x.PhoneNumber == phonenumber);
            if (user != null)
            {
                var historyRecord = await _transactionHistoryService.GetTodayCreditTransactionHistoryByUserId(user.Id);
                
                if (historyRecord.Count > 5)
                    return "You have exhausted your limit";


                var cummulativeCredit = 0.0;
                foreach (TransactionHistory item in historyRecord)
                {
                    cummulativeCredit = cummulativeCredit + item.Amount;
                }

                if (cummulativeCredit + amount >= 2000)
                    return "You have exhausted your limit";

                var isTopupSuccessful = await _userBalanceInformationService.TopUpBalance(user.Id, amount);

                await _transactionHistoryService.CreateTransActionHistory(user.Id, amount, "AirrtimeTopUp", "Credit");

                return "Topup Successful";



            }
            return "User Does not exist";

        }
        public async Task<string> TransferAirtimeTopup(string phonenumber, double amount)
        {

            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: t => t.PhoneNumber == phonenumber );
            if (user != null) 
            {
             var historyReccord = await _transactionHistoryService.GetTodayCreditTransactionHistoryByUserId(user.Id);
                if (historyReccord.Count > 5)
                return "You have exhausted your limit";

                var cummulativeCredit = 0.0;
                foreach(TransactionHistory item in historyReccord)
                { 
                 cummulativeCredit = (cummulativeCredit + item.Amount);
                }
                if (cummulativeCredit + amount >= 2000)
                    return "You have exhusted your limit";

                var Issuccessful = await _userBalanceInformationService.TopUpBalance(user.Id,amount);
                await _transactionHistoryService.CreateTransActionHistory(user.Id, amount,"transfer AirtimeTopup","Credit");
                return "Transfer Topup successful";
            }
            return "Not Successful";

        }
        public async Task<string>MakeACall(string initiatorPhoneNumber, double minutes)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: t=> t.PhoneNumber == initiatorPhoneNumber);
            if (user != null)
            {
                var deductibleAmount = minutes * 10;
                var isDeducted = await _userBalanceInformationService.ChargingCustomer(user.Id, deductibleAmount);
                if (isDeducted == true) 
                {
                    return "Operation is successful";
                }
                return "Error Occur while making call";
            }
            return "User Not Register";
        }
    }
}