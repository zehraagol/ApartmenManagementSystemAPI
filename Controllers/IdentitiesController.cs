using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AparmentSystemAPI.Models.Identities.DTOs;
using AparmentSystemAPI.Models.Identities.Interfaces;
using AparmentSystemAPI.Models.Tokens;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentitiesController(IIdentityService identityService, TokenService tokenService) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto request)
        {
            var response = await identityService.CreateUser(request);

            if (response.AnyError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //update user
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateRequestDto request)
        {
            var response = await identityService.UpdateUser(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //delete user
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserDeleteRequestDto request)
        {
            var response = await identityService.DeleteUser(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}

