using Microsoft.AspNetCore.Identity.UI.Services;
using PostmarkDotNet;
using PostmarkDotNet.Model;

namespace EventManager.Utils
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new PostmarkMessage()
            {
                To = email,
                From = "szymon.fiedorowicz@witkac.pl",
                Subject = subject,
                HtmlBody = htmlMessage,
            };

            var client = new PostmarkClient("84b63a08-aa29-413c-8a01-ae6a01dee7cf");

            return client.SendMessageAsync(message);
        }
    }
}
