using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments.DTOs;
using AparmentSystemAPI.Models.Payments.Interfaces;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace AparmentSystemAPI.Models.Payments
{
    public class PaymentService(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper,
                              IUnitOfWork unitOfWork) : IPaymentService
    {
        private readonly AppDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<ResponseDto<Guid>> AdminPaymentAddAsync(AddPaymentRequestDto request)
        {
            Payment payment = _mapper.Map<Payment>(request);
            // Payment payment = new Payment();
            var flatNo = request.FlatNo;
            var flat = _context.Flats.Include(u => u.User).FirstOrDefault(u => u.FlatNumber == flatNo);

            if (flat == null)
            {
                return ResponseDto<Guid>.Fail("Flat not found!");
            }

            payment.Id = Guid.NewGuid();
            payment.FlatId = flat.Id;
            payment.AppUserId = flat.UserId;
            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<Guid>.Success(payment.Id);
        }

        // create payment for all flats from flat list that has not paymentType = "aidat" and paymentMonth and paymentYear is equal to request
        public async Task<ResponseDto<string>> CreateAidatPaymentForNonPaidFlats(AddAidatPaymentRequestDto request)
        {
            // var flats = _context.Flats.Include(u => u.User).Where(u => u.PaymentType != "aidat").ToList();
            //find list of payments that has not paymentType = "aidat" and paymentMonth and paymentYear is equal to request
            var payments = _context.Payments.Include(u => u.Flat).Where(u => u.PaymentType == "aidat").
                                                                                    Where(p => p.PaymentYear == request.PaymentYear)
                                                                                    .Where(p => p.PaymentMonth == request.PaymentMonth)
                                                                                    .Where(p => p.PaymentDate != null).
                                                                                    ToList();

            // list all flats and find the difference between flats and payments lists
            var flats = _context.Flats.Include(u => u.User).Where(u => u.isEmpty == false).ToList().Except(payments.Select(p => p.Flat).ToList()).ToList();

            foreach (var flat in flats)
            {
                if (flat.BlockInfo == "Apartment")
                {
                    continue;
                }


                Payment payment = new Payment();
                payment.Id = Guid.NewGuid();
                payment.PaymentMonth = request.PaymentMonth;
                payment.PaymentYear = request.PaymentYear;
                payment.PaymentAmount = request.PaymentAmount;
                payment.FlatId = flat.Id;
                payment.AppUserId = flat.UserId;
                // payment.PaymentDate = request.PaymentDate;
                payment.PaymentType = "aidat";
                await _unitOfWork.PaymentRepository.AddAsync(payment);
            }
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<string>.Success("Payments created for all flats");
        }

        // get all payments
        public async Task<ResponseDto<List<PaymentDto>>> GetAllPayments()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllAsync();
            var paymentDtos = _mapper.Map<List<PaymentDto>>(payments);
            return ResponseDto<List<PaymentDto>>.Success(paymentDtos);
        }

        public bool isRegular(AppUser user)
        {
            var paymentType = "aidat";
            var paymentPeriod = DateTime.Now.Month + 12;
            // paymentPeriod is month count. Create a DateTime object with subtracting paymentPeriod from DateTime.Now
            var date = DateTime.Now.AddMonths(-paymentPeriod);

            var allPaymentsInGivenPeriod = _context.Payments.Where(p => p.AppUserId == user.Id)
                                                            .Where(p => p.PaymentType == paymentType)
                                                            .Where(p => p.PaymentDate != null)
                                                            .Where(p => p.PaymentDate.Value.Year >= date.Year)
                                                            .Where(p => p.PaymentDate.Value.Month >= date.Month)
                                                            .Where(p => p.PaymentDate.Value.Year < DateTime.Now.Year)
                                                            .ToList();

            if (allPaymentsInGivenPeriod.Count < 12)
            {
                return false;
            }
            foreach (var payment in allPaymentsInGivenPeriod)
            {
                if (payment.PaymentDate.Value.Year != payment.PaymentYear || payment.PaymentDate.Value.Month != payment.PaymentMonth)
                {
                    return false;
                }
            }

            return true;
        }

        // user can pay payment that has not been paid before only for his/her flat
        public async Task<ResponseDto<string>> PayPayment(PayPaymentRequestDto request)
        {
            var flatNo = request.FlatNo;
            var flat = _context.Flats.Include(u => u.User).FirstOrDefault(u => u.FlatNumber == flatNo);

            if (flat == null)
            {
                return ResponseDto<string>.Fail("Flat not found!");
            }


            var payment = _context.Payments.FirstOrDefault(u => u.FlatId == flat.Id && u.PaymentYear == request.PaymentYear
                                                                                    && u.PaymentMonth == request.PaymentMonth
                                                                                    && u.PaymentType == request.PaymentType);
            if (payment == null)
            {
                return ResponseDto<string>.Fail("Payment not found!");
            }
            if (payment.PaymentDate != null)
            {
                return ResponseDto<string>.Fail("Payment has already been paid!");
            }
            payment.isCreditCard = request.isCreditCard;
            payment.PaymentDate = DateTime.Now;

            // if paymentdate is equal to payment month and payment year, it means it is paid on time. If not paid on time, increase the payment amount by 10%
            if (payment.PaymentDate.Value.Year != payment.PaymentYear || payment.PaymentDate.Value.Month != payment.PaymentMonth)
            {
                payment.PaymentAmount = (int)(payment.PaymentAmount * 1.1);
            }
            // find user with flat.UserId
            var user = _context.Users.FirstOrDefault(u => u.Id == flat.UserId);
            // if user is regular, decrease the payment amount by 10%
            if (isRegular(user) && request.PaymentType == "aidat")
            {
                payment.PaymentAmount = (int)(payment.PaymentAmount * 0.9);
            }

            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<string>.Success("Payment has been paid!");
        }

        // user can see all payments that has been paid for his/her flat
        public ResponseDto<List<PaymentDto>> GetPaymentsByFlatNo(GetPaymentByFlatNoRequestDto request)
        {

            // get the payments of given flat number
            var flatNo = request.FlatNo;
            // find this flat for given flat number

            var flat = _context.Flats.Include(u => u.User).FirstOrDefault(u => u.FlatNumber == flatNo);
            if (flat == null)
            {
                return ResponseDto<List<PaymentDto>>.Fail("Flat not found!");
            }

            List<Payment> payments = new List<Payment>();

            if (request.isPaid)
            {
                payments = _context.Payments.Where(u => u.FlatId == flat.Id)
                                    .Where(p => p.PaymentDate != null)
                                    .Where(string.IsNullOrEmpty(request.PaymentType) ? p => true : p => p.PaymentType == request.PaymentType)
                                    .ToList();
            }

            else
            {
                payments = _context.Payments.Where(u => u.FlatId == flat.Id)
                                    .Where(p => p.PaymentDate == null)
                                    .Where(string.IsNullOrEmpty(request.PaymentType) ? p => true : p => p.PaymentType == request.PaymentType)
                                    .ToList();
            }

            //  var payments = _context.Payments.Where(u => u.FlatId == flat.Id).ToList();
            var paymentDtos = _mapper.Map<List<PaymentDto>>(payments);
            return ResponseDto<List<PaymentDto>>.Success(paymentDtos);
        }

        // get sum of all payments for given flat number within given year or month
        public async Task<ResponseDto<int>> GetTotalPaymentByFlatNo(GetTotalPaymentByFlatNoRequestDto request)
        {
            var flatNo = request.FlatNo;
            var flat = _context.Flats.Include(u => u.User).FirstOrDefault(u => u.FlatNumber == flatNo);
            if (flat == null)
            {
                return ResponseDto<int>.Fail("Flat not found!");
            }
            List<Payment> payments = new List<Payment>();
            if (request.MonthlyOrYearly == "Monthly")
            {
                payments = _context.Payments.Where(u => u.FlatId == flat.Id)
                                     .Where(p => p.PaymentYear == request.PaymentYear)
                                     .Where(p => p.PaymentMonth == request.PaymentMonth)
                                     .Where(p => p.PaymentDate == null)
                                     .Where(p => p.PaymentType == request.PaymentType)
                                     .ToList();

            }

            if (request.MonthlyOrYearly == "Yearly")
            {
                payments = _context.Payments.Where(u => u.FlatId == flat.Id)
                                     .Where(p => p.PaymentYear == request.PaymentYear)
                                     .Where(p => p.PaymentDate == null)
                                     .Where(p => p.PaymentType == request.PaymentType)
                                     .ToList();
            }

            var totalPayment = payments.Sum(p => p.PaymentAmount);
            return ResponseDto<int>.Success(totalPayment);
        }

        // get the users who has paid his/her payments regulary within given time period and given payment type. If the month and year of payment date are the same with payment month and payment year properties, it means it is paid on time.
        public async Task<ResponseDto<List<AppUser>>> RegularyPayingUsers(RegularlyPayingUsersRequestDto request)
        {
            var paymentType = request.PaymentType;
            var paymentPeriod = request.PaymentPeriod;
            // paymentPeriod is month count. Create a DateTime object with subtracting paymentPeriod from DateTime.Now
            var date = DateTime.Now.AddMonths(-paymentPeriod);

            var allPaymentsInGivenPeriod = _context.Payments.Where(p => p.PaymentType == paymentType)
                                                            .Where(p => p.PaymentDate != null)
                                                            .Where(p => p.PaymentDate.Value.Year >= date.Year)
                                                            .Where(p => p.PaymentDate.Value.Month >= date.Month)
                                                            .ToList();
            // find unique users in allPaymentsInGivenPeriod
            var users = allPaymentsInGivenPeriod.Select(p => p.AppUserId).Distinct().ToList();

            // find the users who has paid all payments in given period in time (in the same month with payment year and payment month)
            List<AppUser> regularlyPayingUsers = new List<AppUser>();
            foreach (var user in users)
            {
                var payments = allPaymentsInGivenPeriod.Where(p => p.AppUserId == user).ToList();
                var isRegularyPaying = true;

                if (payments.Count < paymentPeriod)
                {
                    isRegularyPaying = false;
                }
                foreach (var payment in payments)
                {
                    if (payment.PaymentDate.Value.Year != payment.PaymentYear || payment.PaymentDate.Value.Month != payment.PaymentMonth)
                    {
                        isRegularyPaying = false;
                        break;
                    }
                }
                if (isRegularyPaying)
                {
                    var appUser = _context.Users.FirstOrDefault(u => u.Id == user);
                    regularlyPayingUsers.Add(appUser);
                }
            }
            return ResponseDto<List<AppUser>>.Success(regularlyPayingUsers);
        }

        // get the users who has not paid his/her aidat payments for this month

        public async Task<ResponseDto<List<Guid?>>> GetNonPaidAidatUsersForThisMonth() // ekstra ekledim
        {
            var paymentType = "aidat";
            // paymentPeriod is month count. Create a DateTime object with subtracting paymentPeriod from DateTime.Now
            var date = DateTime.Now;

            var allPaymentsInGivenPeriod = _context.Payments.Where(p => p.PaymentType == paymentType)
                                                            .Where(p => p.PaymentDate == null)
                                                            .Where(p => p.PaymentYear == date.Year)
                                                            .Where(p => p.PaymentMonth == date.Month)
                                                            .ToList();
            // find unique users in allPaymentsInGivenPeriod
            var users = allPaymentsInGivenPeriod.Select(p => p.AppUserId).Distinct().ToList();


            return ResponseDto<List<Guid?>>.Success(users);
        }

    }
}
