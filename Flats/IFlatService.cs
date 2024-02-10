using AparmentSystemAPI.Flats.DTOs;
using AparmentSystemAPI.Models.DTOs;

namespace AparmentSystemAPI.Flats
{
    public interface IFlatService
    {
        Task<ResponseDto<Guid>> AddAsync(AddFlatRequestDto request);
        Task<ResponseDto<string>> AddUserToFlatAsync(AddUserToFlatRequestDto request);
        //Task<ResponseDto<Guid>> CreateFlatWithoutUser(CreateFlatWithoutUserRequestDto request);
    }
}
