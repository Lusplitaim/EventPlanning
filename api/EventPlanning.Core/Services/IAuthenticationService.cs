using EventPlanning.Core.DTOs.Auth;

namespace EventPlanning.Core.Services
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto model);
        Task<bool> AuthenticateUserAsync(LoginUserDto model);
        Task<string> CreateTokenAsync(string userEmail);
        Task<bool> ConfirmEmailAsync(string userEmail, string token);
    }
}