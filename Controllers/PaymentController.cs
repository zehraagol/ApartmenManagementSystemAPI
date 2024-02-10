using AparmentSystemAPI.Flats;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.DTOs;
using AparmentSystemAPI.Payments;
using AparmentSystemAPI.Payments.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
            [Authorize(Roles = "admin")]
             [HttpPost]
            public Task<ResponseDto<Guid>> AdminPaymentAdd(AddPaymentRequestDto request)
            {
                return paymentService.AdminPaymentAddAsync(request);
            }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<string>> CreateAidatPaymentForNonPaidFlats(AddAidatPaymentRequestDto request)
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
        public ResponseDto<List<PaymentDto>> GetPaymentsByFlatNo(GetPaymentByFlatNoRequestDto request)
        {
            return paymentService.GetPaymentsByFlatNo(request);
        }

        //pay payment
        [Authorize(Roles = "admin,kullanici")]
        [HttpPost]
        public Task<ResponseDto<string>> PayPayment(PayPaymentRequestDto request)
        {
            return paymentService.PayPayment(request);
        }

        //get total payment by flatno
        [Authorize(Roles = "admin,kullanici")]
        [HttpPost]
        public Task<ResponseDto<int>> GetTotalPaymentByFlatNo(GetTotalPaymentByFlatNoRequestDto request)
        {
            return paymentService.GetTotalPaymentByFlatNo(request);
        }

        //get regularly paying users
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<List<AppUser>>> RegularyPayingUsers(RegularlyPayingUsersRequestDto request)
        {
            return paymentService.RegularyPayingUsers(request);
        }

        //get non paid aidat users for this month
        [Authorize(Roles = "admin")]
        [HttpGet]
        public Task<ResponseDto<List<Guid?>>> GetNonPaidAidatUsersForThisMonth()
        {
            return paymentService.GetNonPaidAidatUsersForThisMonth();
        }

        }
    }
    
        
    

