using AparmentSystemAPI.Models.DTOs;
using AparmentSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AparmentSystemAPI.Tokens.DTOs;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace AparmentSystemAPI.Tokens
{
    public class TokenService(IConfiguration configuration, UserManager<AppUser> userManager, AppDbContext _context)
    {
        public async Task<ResponseDto<TokenCreateResponseDto>> Create(TokenCreateRequestDto request)
        {
        
            var hasUser = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber && x.TCNumber == request.TcNumber);


            if (hasUser is null)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("TC or Phone Number is wrong!");
            }



            var signatureKey = configuration.GetSection("TokenOptions")["SignatureKey"]!;
            var tokenExpireAsHour = configuration.GetSection("TokenOptions")["Expire"]!;
            var issuer = configuration.GetSection("TokenOptions")["Issuer"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));

            //payload => list claim Data(Key-value)
            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();

            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, hasUser.UserName!);
            //var phoneNumberAsClaim = new Claim(ClaimTypes.MobilePhone, hasUser.PhoneNumber!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());


            var userClaims = await userManager.GetClaimsAsync(hasUser);

            foreach (var claim in userClaims)
            {
                claimList.Add(new Claim(claim.Type, claim.Value));
            }


            claimList.Add(userIdAsClaim);
            //claimList.Add(phoneNumberAsClaim);
            claimList.Add(userNameAsClaim);
            claimList.Add(idAsClaim);

            foreach (var role in await userManager.GetRolesAsync(hasUser))
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(Convert.ToDouble(tokenExpireAsHour)),
                signingCredentials: signingCredentials,
                claims: claimList,
                issuer: issuer
            );

            var responseDto = new TokenCreateResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return ResponseDto<TokenCreateResponseDto>.Success(responseDto);
        }

       //--------------------------------------------------------------------------------
        public async Task<ResponseDto<TokenCreateResponseDto>> CreateAdminToken(AdminTokenCreateRequestDto request)
        {
            var hasUser = await userManager.FindByNameAsync(request.UserName);

            if (hasUser is null)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong");
            }

            var checkPassword = await userManager.CheckPasswordAsync(hasUser!, request.Password);

            if (checkPassword == false)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong");
            }


            var signatureKey = configuration.GetSection("TokenOptions")["SignatureKey"]!;
            var tokenExpireAsHour = configuration.GetSection("TokenOptions")["Expire"]!;
            var issuer = configuration.GetSection("TokenOptions")["Issuer"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));

            //payload => list claim Data(Key-value)
            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();

            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, hasUser.UserName!);
            //var phoneNumberAsClaim = new Claim(ClaimTypes.MobilePhone, hasUser.PhoneNumber!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());




            //var userClaims = await userManager.GetClaimsAsync(hasUser);

            //foreach (var claim in userClaims)
            //{
            //    claimList.Add(new Claim(claim.Type, claim.Value));
            //}


            claimList.Add(userIdAsClaim);
            //claimList.Add(phoneNumberAsClaim);
            claimList.Add(userNameAsClaim);
            claimList.Add(idAsClaim);

            foreach (var role in await userManager.GetRolesAsync(hasUser))
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(Convert.ToDouble(tokenExpireAsHour)),
                signingCredentials: signingCredentials,
                claims: claimList,
                issuer: issuer
            );

            var responseDto = new TokenCreateResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };


            return ResponseDto<TokenCreateResponseDto>.Success(responseDto);
        }

    }
}

