using EventPlanning.Core.DTOs.User;
using EventPlanning.Core.Storages;
using EventPlanning.Core.Exceptions;

namespace EventPlanning.Core.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUserStorage m_UserStorage;
        public UsersService(IUserStorage userStorage)
        {
            m_UserStorage = userStorage;
        }

        public async Task<UserDto> GetUserAsync(string userEmail)
        {
            try
            {
                var result = await m_UserStorage.GetAsync(userEmail);
                if (result is not null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get user", ex);
            }

            throw new NotFoundCoreException();
        }
    }
}
