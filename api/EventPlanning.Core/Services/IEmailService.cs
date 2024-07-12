namespace EventPlanning.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string content, string email, string userName);
    }
}