using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService m_AuthService;
        public AuthenticationController(IAuthenticationService service)
        {
            m_AuthService = service;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromForm] LoginUserDto model)
        {
            var success = await m_AuthService.AuthenticateUserAsync(model);
            if (success)
            {
                return Ok(await m_AuthService.CreateTokenAsync(model.Email));
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserDto model)
        {
            var result = await m_AuthService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(RegisterUser), default);
            }

            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
