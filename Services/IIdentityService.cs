using AparmentSystemAPI.Models.DTOs;
using AparmentSystemAPI.Tokens.DTOs;

namespace AparmentSystemAPI.Services
{
    public interface IIdentityService
    {
        Task<ResponseDto<Guid>> CreateUser(UserCreateRequestDto request);

        Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request);

        Task<ResponseDto<string>> UpdateUser(UserUpdateRequestDto request);

        Task<ResponseDto<string>> DeleteUser(UserDeleteRequestDto request);
    }
}
