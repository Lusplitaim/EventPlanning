using EventPlanning.Core.Data.Entities;
using EventPlanning.Core.DTOs.Auth;
using EventPlanning.Core.Exceptions;
using EventPlanning.Core.Models.Options;
using EventPlanning.Core.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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
        private readonly IEmailService m_EmailService;
        private readonly IUrlHelperFactory m_UrlHelperFactory;
        private readonly IActionContextAccessor m_ActionContextAccessor;
        private readonly IHttpContextAccessor m_HttpContextAccessor;
        public AuthenticationService(
            IUserStorage userStorage,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<JwtOptions> opts,
            IEmailService emailService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            m_UserStorage = userStorage;
            m_UserManager = userManager;
            m_SignInManager = signInManager;
            m_JwtOptions = opts.Value;
            m_EmailService = emailService;
            m_UrlHelperFactory = urlHelperFactory;
            m_ActionContextAccessor = actionContextAccessor;
            m_HttpContextAccessor = httpContextAccessor;
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

        public async Task<bool> ConfirmEmailAsync(string userEmail, string token)
        {
            try
            {
                var user = await m_UserManager.FindByEmailAsync(userEmail);
                if (user is not null)
                {
                    var result = await m_UserManager.ConfirmEmailAsync(user, token);
                    return result.Succeeded;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to confirm email", ex);
            }

            throw new NotFoundCoreException();
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

        public async Task<bool> RegisterUserAsync(RegisterUserDto model)
        {
            try
            {
                var registerResult = await m_UserStorage.CreateAsync(model);
                if (!registerResult.Succeeded)
                {
                    return false;
                }

                var user = await m_UserManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    return false;
                }

                var token = await m_UserManager.GenerateEmailConfirmationTokenAsync(user);
                var urlHelper = m_UrlHelperFactory.GetUrlHelper(m_ActionContextAccessor.ActionContext!);
                var confirmationLink = urlHelper.Action(new()
                {
                    Action = "ConfirmEmail",
                    Controller = "Authentication",
                    Values = new { token, email = user.Email },
                    Protocol = m_HttpContextAccessor.HttpContext!.Request.Scheme,
                });
                await m_EmailService.SendEmailConfirmationAsync(confirmationLink!, user.Email, user.UserName);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user", ex);
            }
        }
    }
}
