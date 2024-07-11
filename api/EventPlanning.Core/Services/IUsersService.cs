using EventPlanning.Core.DTOs.User;

namespace EventPlanning.Core.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(string userEmail);
    }
}