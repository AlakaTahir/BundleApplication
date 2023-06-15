using Bundle.Model.ViewModel;
using Bundle.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bundle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundleInformationController : ControllerBase
    {
        private readonly IBundleInformationService _bundleInformationService;
        public BundleInformationController(IBundleInformationService bundleInformationService)
        {
            _bundleInformationService = bundleInformationService;
        }
        [HttpPost("CreateUser")]

        public async Task<IActionResult> CreateUser(BundleInformationRequestModel model)
        {
            var response = await _bundleInformationService.CreateUser(model);
            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, BundleInformationRequestModel model)
        {
            var response = await _bundleInformationService.UpdateUser(id, model);
            return Ok(response);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserDeleteUser(Guid id)
        {
            var response = await _bundleInformationService.DeleteUser(id);
            return Ok(response);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _bundleInformationService.GetUserById(id);
            return Ok(response);

        }
        [HttpGet("BalanceByPhone")]
        public async Task<IActionResult> RetrieveBalanceByPhoneNumber(string phoneNumber)
        {
            var response = await _bundleInformationService.RetrieveBalanceByPhoneNumber(phoneNumber);
            return Ok(response);
        }
        [HttpPost("AirtimeTopUp")]
        public async Task<IActionResult> AirtimeTopup(string phoneNumber, double amount)
        {
            var response = await _bundleInformationService.AirtimeTopup(phoneNumber, amount);
            return Ok(response);
        }
        [HttpPost("TransferAirtimeTopUp")]
        public async Task<IActionResult> TransferAirtimeTopup(string phoneNumber, double amount)
        {
            var response = await _bundleInformationService.TransferAirtimeTopup(phoneNumber, amount);
            return Ok(response);
        }
        [HttpPost("MakeACall")]
        public async Task<IActionResult> MakeACall(string initiatorPhonenumber, double minutes)
        {
            var response = await _bundleInformationService.MakeACall(initiatorPhonenumber, minutes);
            return Ok(response);

        }
    }
}
