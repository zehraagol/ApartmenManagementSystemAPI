using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.MainBuildings.DTOs;
using AparmentSystemAPI.Models.MainBuildings.Interfaces;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class MainBuildingService(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper,
                                IUnitOfWork unitOfWork) : IMainBuildingService
{
    private readonly AppDbContext _context = context;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork; 

    // add payment to main building with given dto
    public async Task<ResponseDto<Guid>> AddPaymentAsync(AddPaymentToMainBuildingRequestDto request)
    {

        // find main building by name "apartment"
        var mainBuilding = await _context.MainBuildings.FirstOrDefaultAsync(u => u.Name == "apartment");

        if (mainBuilding == null)
        {
            return ResponseDto<Guid>.Fail("Main building not found!");
        }

        // create a flat named tmpFlatForApartment

        // if Apartment flat exists, return error
        var tmpFlat = await _context.Flats.FirstOrDefaultAsync(u => u.FlatType == "Apartment");

        Flat tmpFlatForApartment = new Flat();
        if (tmpFlat == null)
        {
            tmpFlatForApartment.Id = Guid.NewGuid();
            tmpFlatForApartment.FlatType = "Apartment";
            tmpFlatForApartment.BlockInfo = "A";
            tmpFlatForApartment.FlatNumber = 0;
            tmpFlatForApartment.FloorNumber = "Apartment";
            // add the flat to flats table
            await _context.Flats.AddAsync(tmpFlatForApartment);
            await _context.SaveChangesAsync();
        }
        else
        {
            tmpFlatForApartment = tmpFlat;
        }


        // create payment object and map it with given dto not use mapper
        Payment payment = new Payment();
        payment.Id = Guid.NewGuid();
        payment.PaymentYear = request.PaymentYear;
        payment.PaymentMonth = request.PaymentMonth;
        payment.PaymentType = request.PaymentType;
        payment.PaymentAmount = request.PaymentAmount;
        payment.MainBuildingId = mainBuilding.Id;
        payment.FlatId = tmpFlatForApartment.Id;

        await _context.Payments.AddAsync(payment);    
        await _context.SaveChangesAsync();

        // get all flats from flats table and add the same payment to all flats
        var flats = await _context.Flats.Where(u => u.UserId != null).ToListAsync();
        // split the amount to all flats
        var paymentAmount = request.PaymentAmount / flats.Count;
        foreach (var flat in flats)
        {
            Payment paymentToFlat = new Payment();
            paymentToFlat.Id = Guid.NewGuid();
            paymentToFlat.PaymentYear = request.PaymentYear;
            paymentToFlat.PaymentMonth = request.PaymentMonth;
            paymentToFlat.PaymentType = request.PaymentType;
            paymentToFlat.PaymentAmount = paymentAmount;
            paymentToFlat.FlatId = flat.Id;
            await _context.Payments.AddAsync(paymentToFlat);
            await _context.SaveChangesAsync();
        }
        
        return ResponseDto<Guid>.Success(payment.Id);

    }

    // get all payments from main building
    public async Task<ResponseDto<List<Payment>>> GetAllPaymentsAsync()
    {
        // find main building by name "apartment"
        var mainBuilding = await _context.MainBuildings.FirstOrDefaultAsync(u => u.Name == "apartment");
        if (mainBuilding == null)
        {
               return ResponseDto<List<Payment>>.Fail("Main building not found!");
        }

        var payments = await _context.Payments.Where(u => u.MainBuildingId == mainBuilding.Id).ToListAsync();
        return ResponseDto<List<Payment>>.Success(payments);
    }

}