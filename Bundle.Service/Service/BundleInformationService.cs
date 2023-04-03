using Arch.EntityFrameworkCore.UnitOfWork;
using Bundle.Project.Entity;
using Bundle.Project.ViewModel;
using Bundle.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundle.Service.Service
{
    public class BundleInformationService : IBundleInformationService
    {
        public readonly IUnitOfWork _unitOfWork;

        public BundleInformationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseViewModel> CreateUser (BundleInformationRequestModel model) 
         
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y =>y.Email == model.Email);
            if (user == null) 
            { 
                  var newUser = new UserInfo ();
                  newUser.Id = Guid.NewGuid();
                  newUser.Email = model.Email;
                  newUser.Name = model.Name;
                  newUser.PhoneNumber = model.PhoneNumber;
                  newUser.Createddate = DateTime.Now;

                   _unitOfWork.GetRepository<UserInfo>().Insert(newUser);
                   await _unitOfWork.SaveChangesAsync();

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
        public async Task<BaseResponseViewModel> UpdateUser (Guid id, BundleInformationRequestModel model) 
        {
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y=> y.Id == id);
            if (user != null)
            {
                user.Email = model.Email;
                user.Name = model.Name;
                user.PhoneNumber = model.PhoneNumber;

                _unitOfWork.GetRepository<UserInfo>().Update(user);
                _unitOfWork.SaveChanges();

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
        public async Task<BaseResponseViewModel> DeleteUser (Guid id) 
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
            var user = _unitOfWork.GetRepository<UserInfo>().GetFirstOrDefault(predicate: y=> y.Id == id);
            if(user != null)
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
        
       
    }
}
