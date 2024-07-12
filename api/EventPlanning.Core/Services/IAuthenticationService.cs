using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Models;

namespace EventPlanning.Core.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResult> RegisterUserAsync(RegisterUserDto model);
        Task<bool> AuthenticateUserAsync(LoginUserDto model);
        Task<string> CreateTokenAsync(string userEmail);
        Task<bool> ConfirmEmailAsync(string userEmail, string token);
    }
}