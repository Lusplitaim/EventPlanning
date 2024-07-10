using EventPlanning.Core.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto model);
        Task<bool> AuthenticateUserAsync(LoginUserDto model);
        Task<string> CreateTokenAsync(string userEmail);
    }
}