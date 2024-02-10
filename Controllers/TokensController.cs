using AparmentSystemAPI.Models.Tokens.DTOs;
using AparmentSystemAPI.Models.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AparmentSystemAPI.Models.Identities.Interfaces;

namespace AparmentSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokensController(IIdentityService identityService, TokenService tokenService) : Controller
    {

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateTokenForUsers(TokenCreateRequestDto request)
        {
            var response = await tokenService.Create(request);
            if (response.AnyError)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
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

    }
}
