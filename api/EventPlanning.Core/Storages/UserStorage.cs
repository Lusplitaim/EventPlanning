using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.DTOs.User;
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

        public async Task<UserDto?> GetAsync(string email)
        {
            var userEntity = await m_UserManager.FindByEmailAsync(email);
            if (userEntity is null)
            {
                return null;
            }

            UserDto result = new()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
            };

            return result;
        }
    }
}
