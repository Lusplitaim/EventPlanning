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
        private readonly IUsersService m_UsersService;
        public AuthenticationController(IAuthenticationService service, IUsersService usersService)
        {
            m_AuthService = service;
            m_UsersService = usersService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginUserDto model)
        {
            var success = await m_AuthService.AuthenticateUserAsync(model);
            if (success)
            {
                var token = await m_AuthService.CreateTokenAsync(model.Email);
                var user = await m_UsersService.GetUserAsync(model.Email);
                return Ok(new { user, token });
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDto model)
        {
            var result = await m_AuthService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                var token = await m_AuthService.CreateTokenAsync(model.Email);
                var user = await m_UsersService.GetUserAsync(model.Email);
                return CreatedAtAction(nameof(RegisterUser), new { user, token });
            }

            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
