using AparmentSystemAPI.Models.MainBuildings.DTOs;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Shared;

namespace AparmentSystemAPI.Models.MainBuildings.Interfaces

{
    public interface IMainBuildingService
    {
        Task<ResponseDto<List<Payment>>> GetAllPaymentsAsync();
        Task<ResponseDto<Guid>> AddPaymentAsync(AddPaymentToMainBuildingRequestDto request);

    }
}
