using AparmentSystemAPI.Models.Identities.DTOs;
using AparmentSystemAPI.Models.Shared;
using AparmentSystemAPI.Models.Tokens.DTOs;

namespace AparmentSystemAPI.Models.Identities.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<Guid>>  CreateUser(UserCreateRequestDto request);
        Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request);
        Task<ResponseDto<string>> UpdateUser(UserUpdateRequestDto request);
        Task<ResponseDto<string>> DeleteUser(UserDeleteRequestDto request);
    }
}
