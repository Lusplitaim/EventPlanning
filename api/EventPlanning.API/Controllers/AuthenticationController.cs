using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService m_AuthService;
        public AuthenticationController(IAuthenticationService service)
        {
            m_AuthService = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDto model)
        {
            var result = await m_AuthService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(RegisterUser), default);
            }
            
            return BadRequest();
        }
    }
}
