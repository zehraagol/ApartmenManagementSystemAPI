using AparmentSystemAPI.Models.DTOs;
using AparmentSystemAPI.Services;
using AparmentSystemAPI.Tokens.DTOs;
using AparmentSystemAPI.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AparmentSystemAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentitiesController(IIdentityService identityService, TokenService tokenService) : ControllerBase
    {

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto request)
        {
            var response = await identityService.CreateUser(request);

            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(TokenCreateRequestDto request)
        {
            var response = await tokenService.Create(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(RoleCreateRequestDto request)
        {
            var response = await identityService.CreateRole(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenForAdmin(AdminTokenCreateRequestDto request)
        {
            var response = await tokenService.CreateAdminToken(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //update user
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

