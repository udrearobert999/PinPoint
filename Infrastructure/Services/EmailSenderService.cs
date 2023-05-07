using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.Services
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email sending logic

            return Task.CompletedTask;
        }
    }
}
