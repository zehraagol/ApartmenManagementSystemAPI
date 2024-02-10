using AparmentSystemAPI.Models.Flats.DTOs;
using AparmentSystemAPI.Models.Shared;

namespace AparmentSystemAPI.Models.Flats.Interfaces
{
    public interface IFlatService
    {
        Task<ResponseDto<Guid>> AddAsync(AddFlatRequestDto request);
        Task<ResponseDto<string>> AddUserToFlatAsync(AddUserToFlatRequestDto request);
        //Task<ResponseDto<Guid>> CreateFlatWithoutUser(CreateFlatWithoutUserRequestDto request);
    }
}
