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
            var regResult = await m_AuthService.RegisterUserAsync(model);
            if (regResult.Success)
            {
                return CreatedAtAction(nameof(RegisterUser), new { emailLink = regResult.Result });
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            await m_AuthService.ConfirmEmailAsync(email, token);
            return Ok("Email is confirmed");
        }
    }
}
