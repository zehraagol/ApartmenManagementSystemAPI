using AparmentSystemAPI.Models.Payments.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.Payments.Interfaces;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController(IPaymentService paymentService) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<Guid>> AssingBillToUser(AddPaymentRequestDto request)
        {
            return paymentService.AdminPaymentAddAsync(request);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<string>> AssingAidatPaymentsToAllUnpaidFlats(AddAidatPaymentRequestDto request)
        {
            return paymentService.CreateAidatPaymentForNonPaidFlats(request);
        }

        //get all payments
        [Authorize(Roles = "admin")]
        [HttpGet]
        public Task<ResponseDto<List<PaymentDto>>> GetAllPayments()
        {
            return paymentService.GetAllPayments();
        }

        //get all payments by flatno
        [Authorize(Roles = "admin,kullanici")]
        [HttpPost]
        public ResponseDto<List<PaymentDto>> GetPaymentsWithGivenFlatNumber(GetPaymentByFlatNoRequestDto request)
        {
            return paymentService.GetPaymentsByFlatNo(request);
        }

        //pay payment
        [Authorize(Roles = "admin,kullanici")]
        [HttpPost]
        public Task<ResponseDto<string>> PayBillWithGivenFlatNumber(PayPaymentRequestDto request)
        {
            return paymentService.PayPayment(request);
        }

        //get total payment by flatno
        [Authorize(Roles = "admin,kullanici")]
        [HttpPost]
        public Task<ResponseDto<int>> GetTotalPaymentAmountWithGivenFlatNumber(GetTotalPaymentByFlatNoRequestDto request)
        {
            return paymentService.GetTotalPaymentByFlatNo(request);
        }

        //get regularly paying users
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<List<AppUser>>> GetUsersPayingRegularly(RegularlyPayingUsersRequestDto request)
        {
            return paymentService.RegularyPayingUsers(request);
        }

        //get non paid aidat users for this month
        [Authorize(Roles = "admin")]
        [HttpGet]
        public Task<ResponseDto<List<Guid?>>> GetUsersHavingUnpaidAidatPaymentForCurrentMonth()
        {
            return paymentService.GetNonPaidAidatUsersForThisMonth();
        }

    }
}
