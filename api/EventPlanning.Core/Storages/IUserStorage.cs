using EventPlanning.Core.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Storages
{
    public interface IUserStorage
    {
        Task<IdentityResult> CreateAsync(RegisterUserDto model);
    }
}
