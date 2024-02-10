using AparmentSystemAPI.Models.Flats.DTOs;
using AparmentSystemAPI.Models.Flats.Interfaces;
using AparmentSystemAPI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlatsController(IFlatService flatService) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<Guid>> AddEmptyFlat(AddFlatRequestDto request)
        {
            return flatService.AddAsync(request);
        }

        //add user to flat
        [Authorize(Roles = "admin")]
        [HttpPost]
        public Task<ResponseDto<string>> AddUserToEmptyFlatAsync(AddUserToFlatRequestDto request)
        {
            return flatService.AddUserToFlatAsync(request);
        }

    }
}
