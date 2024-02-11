using AparmentSystemAPI.Models.Identities.DTOs;
using AparmentSystemAPI.Models.Identities.Interfaces;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.Tokens.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AparmentSystemAPI.Models.Identities
{
    public class IdentityService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
                                                         AppDbContext context, IMapper mapper) : IIdentityService
    {
        public UserManager<AppUser> UserManager { get; set; } = userManager;
        public RoleManager<AppRole> RoleManager { get; set; } = roleManager;

        #region create user
        public async Task<ResponseDto<Guid>> CreateUser(UserCreateRequestDto request)
        {

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.FullName,
                TCNumber = request.TCNumber,
                PhoneNumber = request.PhoneNumber,
                   
            };

            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(e => e.Description).ToList();
                return ResponseDto<Guid>.Fail(errorList);

            }

            return ResponseDto<Guid>.Success(user.Id);
        }
        #endregion

        #region create role
        public async Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request)
        {
            var appRole = new AppRole
            {
                Name = request.RoleName
            };


            var hasRole = await roleManager.RoleExistsAsync(appRole.Name);

            IdentityResult? roleCreateResult = null;
            if (!hasRole)
            {
                roleCreateResult = await roleManager.CreateAsync(appRole);
            }


            if (roleCreateResult is not null && !roleCreateResult.Succeeded)
            {
                var errorList = roleCreateResult.Errors.Select(x => x.Description).ToList();

                return ResponseDto<string>.Fail(errorList);
            }


            var hasUser = await userManager.FindByIdAsync(request.UserId);

            if (hasUser is null)
            {
                return ResponseDto<string>.Fail("kullanıcı bulunamadı.");
            }


            var roleAssignResult = await userManager.AddToRoleAsync(hasUser, appRole.Name);

            if (!roleAssignResult.Succeeded)
            {
                var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();

                return ResponseDto<string>.Fail(errorList);
            }

            return ResponseDto<string>.Success(string.Empty);
        }
        #endregion


        #region update user

        public async Task<ResponseDto<string>> UpdateUser(UserUpdateRequestDto request)
        {
            //find user by given tc number with using userManager.Users.Where() method  
            var hasUser = await userManager.Users.Where(x => x.TCNumber == request.TCNumber).FirstOrDefaultAsync();


            if (hasUser is null)
            {
                return ResponseDto<string>.Fail("kullanıcı bulunamadı.");
            }

            hasUser.Email = request.Email;
            hasUser.UserName = request.FullName;
            hasUser.PhoneNumber = request.PhoneNumber;

            var result = await userManager.UpdateAsync(hasUser);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(e => e.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }

            return ResponseDto<string>.Success("Kullanıcı başarıyla güncellendi.");
        }
        #endregion

        
        #region delete user
        public async Task<ResponseDto<string>> DeleteUser(UserDeleteRequestDto request)
        {
            var hasUser = await userManager.Users.Where(x => x.TCNumber == request.TCNumber).FirstOrDefaultAsync();

            if (hasUser is null)
            {
                return ResponseDto<string>.Fail("kullanıcı bulunamadı.");
            }

            //if this user has flats remove this users id from flats
            var hasFlats = await context.Flats.Where(x => x.UserId == hasUser.Id).ToListAsync();
            if (hasFlats is not null)
            {
                foreach (var flat in hasFlats)
                {
                    flat.UserId = null;
                }
            }

            // delete also references in payments
            var hasPayments = await context.Payments.Where(x => x.AppUserId == hasUser.Id).ToListAsync();
            if (hasPayments is not null)
            {
                foreach (var payment in hasPayments)
                {
                    payment.AppUserId = null;
                }
            }

            var result = await userManager.DeleteAsync(hasUser);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(e => e.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }

            return ResponseDto<string>.Success("Kullanıcı başarıyla silindi.");
        }
        #endregion

    }
}
