using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Storages;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserStorage m_UserStorage;
        public AuthenticationService(IUserStorage userStorage)
        {
            m_UserStorage = userStorage;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto model)
        {
            try
            {
                return await m_UserStorage.CreateAsync(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user", ex);
            }
        }
    }
}
