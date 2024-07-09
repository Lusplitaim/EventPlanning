using EventPlanning.Core.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto model);
    }
}