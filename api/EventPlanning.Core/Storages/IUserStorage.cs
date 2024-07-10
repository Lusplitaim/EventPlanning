using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Storages
{
    public interface IUserStorage
    {
        Task<IdentityResult> CreateAsync(RegisterUserDto model);
        Task<UserDto?> GetAsync(string email);
    }
}
