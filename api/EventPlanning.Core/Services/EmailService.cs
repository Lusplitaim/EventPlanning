using MailKit.Net.Smtp;
using MimeKit;

namespace EventPlanning.Core.Services
{
    internal class EmailService : IEmailService
    {
        public async Task SendEmailConfirmationAsync(string content, string email, string userName)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Event Planning App", "eventplanning@test.com"));
                message.To.Add(new MailboxAddress(userName, email));
                message.Subject = "Email Confirmation";
                message.Body = new TextPart("plain") { Text = content };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.test.com", 587, false);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email", ex);
            }
        }
    }
}
