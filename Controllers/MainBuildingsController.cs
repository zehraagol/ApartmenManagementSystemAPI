using AparmentSystemAPI.Models.MainBuildings.DTOs;
using AparmentSystemAPI.Models.MainBuildings.Interfaces;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainBuildingsController(IMainBuildingService mainBuildingService) : Controller
    {

        [Authorize(Roles = "admin")]
        // get all payments
        [HttpGet]
        public Task<ResponseDto<List<Payment>>> GetAllApartmentPaymentsAsync()
        {
            return mainBuildingService.GetAllPaymentsAsync();
        }

        // ad payment to main building
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<Guid>> AddPaymentAsync(AddPaymentToMainBuildingRequestDto request)
        {
            return mainBuildingService.AddPaymentAsync(request);
        }


    }
}
