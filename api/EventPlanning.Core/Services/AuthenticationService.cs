using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Exceptions;
using EventPlanning.Core.Models.Options;
using EventPlanning.Core.Storages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlanning.Core.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUserStorage m_UserStorage;
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly SignInManager<UserEntity> m_SignInManager;
        private readonly JwtOptions m_JwtOptions;
        public AuthenticationService(
            IUserStorage userStorage,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<JwtOptions> opts)
        {
            m_UserStorage = userStorage;
            m_UserManager = userManager;
            m_SignInManager = signInManager;
            m_JwtOptions = opts.Value;
        }

        public async Task<bool> AuthenticateUserAsync(LoginUserDto model)
        {
            try
            {
                if (model is null) throw new ArgumentNullException(nameof(model));

                var result = false;

                var existingUser = await m_UserManager.FindByEmailAsync(model.Email);
                if (existingUser is null)
                {
                    return result;
                }

                var signInResult = await m_SignInManager.CheckPasswordSignInAsync(existingUser, model.Password, false);
                if (signInResult.Succeeded)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to login user", ex);
            }
        }

        public async Task<string> CreateTokenAsync(string userEmail)
        {
            try
            {
                var user = await m_UserStorage.GetAsync(userEmail);

                if (user is not null)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
                    var jwt = new JwtSecurityToken(
                        issuer: m_JwtOptions.ValidIssuer,
                        audience: m_JwtOptions.ValidAudience,
                        claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_JwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create a token", ex);
            }

            throw new NotFoundCoreException();
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
