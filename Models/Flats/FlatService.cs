using AparmentSystemAPI.Models.Flats.DTOs;
using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AparmentSystemAPI.Models.Flats
{
    public class FlatService(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper,
                              IUnitOfWork unitOfWork) : IFlatService
    {
        private readonly AppDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        //public async Task<ResponseDto<Guid>> AddAsync(AddFlatRequestDto request)
        //{
        //    Flat flat = new Flat();
        //    // initialize without mapper

        //    var TcNumber = request.UserTCNumber;
        //    var user = _context.Users.Where(u => u.TCNumber == TcNumber).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return ResponseDto<Guid>.Fail("User not found!");
        //    }

        //    if (flat == null)
        //    {
        //        return ResponseDto<Guid>.Fail("Flat not found!");
        //    }

        //    if (flat != null)
        //    {
        //        flat.Id = Guid.NewGuid();
        //        flat.BlockInfo = request.BlockInfo;
        //        flat.FlatNumber = request.FlatNumber;
        //        flat.FloorNumber = request.FloorNumber;
        //        flat.BlockInfo = request.BlockInfo;
        //        flat.FlatType = request.FlatType;
        //        flat.isEmpty = request.isEmpty;
        //        flat.UserId = user.Id;


        //        await _unitOfWork.Flat.AddAsync(flat);
        //        await _unitOfWork.SaveChangesAsync();
        //        return ResponseDto<Guid>.Success(flat.Id);
        //    }
        //    return ResponseDto<Guid>.Fail("Empty Product!");
        //}



        // add flat without user using mapper
        public async Task<ResponseDto<Guid>> AddAsync(AddFlatRequestDto request)
        {
            Flat flat = _mapper.Map<Flat>(request);
            flat.Id = Guid.NewGuid();
            flat.isEmpty = true;
            await _unitOfWork.Flat.AddAsync(flat);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<Guid>.Success(flat.Id);
        }

        // add user to flat using mapper
        public async Task<ResponseDto<string>> AddUserToFlatAsync(AddUserToFlatRequestDto request)
        {
            var flat = await _context.Flats.FirstOrDefaultAsync(u => u.FlatNumber == request.FlatNumber);

            if (flat == null)
            {
                return ResponseDto<string>.Fail("Flat not found!");
            }
            var user = await _userManager.Users.Where(u => u.TCNumber == request.UserTCNumber).FirstOrDefaultAsync(); //useri bulduk
            if (user == null)
            {
                return ResponseDto<string>.Fail("User not found!");
            }
            flat.UserId = user.Id;
            flat.isEmpty = false;
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<string>.Success("User added to flat successfully");
        }

    }

}

