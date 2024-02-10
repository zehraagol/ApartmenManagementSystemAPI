using AparmentSystemAPI.Flats;
using AparmentSystemAPI.Flats.DTOs;
using AparmentSystemAPI.Models;
using AparmentSystemAPI.Models.DTOs;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace AparmentSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlatsController(IFlatService flatService) : ControllerBase
    {
        [HttpPost]
        public Task<ResponseDto<Guid>> AddAsync(AddFlatRequestDto request)
        {
            return flatService.AddAsync(request);
        }

        //add user to flat
        [HttpPost]
        public Task<ResponseDto<string>> AddUserToFlatAsync(AddUserToFlatRequestDto request)
        {
            return flatService.AddUserToFlatAsync(request);
        }

    }
}
