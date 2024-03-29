﻿using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.DTOs;
using AparmentSystemAPI.Models.Shared;

namespace AparmentSystemAPI.Models.Payments.Interfaces
{
    public interface IPaymentService
    {
        Task<ResponseDto<Guid>> AdminPaymentAddAsync(AddPaymentRequestDto request);
        Task<ResponseDto<string>> CreateAidatPaymentForNonPaidFlats(AddAidatPaymentRequestDto request);
        Task<ResponseDto<List<PaymentDto>>> GetAllPayments();

        Task<ResponseDto<string>> PayPayment(PayPaymentRequestDto request);
        ResponseDto<List<PaymentDto>> GetPaymentsByFlatNo(GetPaymentByFlatNoRequestDto request);
        Task<ResponseDto<int>> GetTotalPaymentByFlatNo(GetTotalPaymentByFlatNoRequestDto request);

        Task<ResponseDto<List<AppUser>>> RegularyPayingUsers(RegularlyPayingUsersRequestDto request); //get regularly paying users

        Task<ResponseDto<List<Guid?>>> GetNonPaidAidatUsersForThisMonth(); // bu ay aidat odemeyenleri getir(ekstra yaptım)
    }
}
