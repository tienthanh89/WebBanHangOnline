using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebBanHangOnline.Models.Entity
{
    public class TbEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // logic to send email
            return Task.CompletedTask;
        }
    }
}
