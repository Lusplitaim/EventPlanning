using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Core.Storages
{
    internal class UserStorage : IUserStorage
    {
        private readonly UserManager<UserEntity> m_UserManager;
        public UserStorage(UserManager<UserEntity> userManager)
        {
            m_UserManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(RegisterUserDto model)
        {
            UserEntity user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            return await m_UserManager.CreateAsync(user, model.Password);
        }
    }
}
