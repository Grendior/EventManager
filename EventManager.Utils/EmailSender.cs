using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using PostmarkDotNet;
using PostmarkDotNet.Model;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace EventManager.Utils
{
    public class EmailSender : IEmailSender
    {
        public System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            Configuration.Default.ApiKey.Add("api-key", "YOUR API KEY");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "John Doe";
            string SenderEmail = "example@example.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = "example1@example1.com";
            string ToName = "John Doe";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            string BccName = "Janice Doe";
            string BccEmail = "example2@example2.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            Bcc.Add(BccData);
            string CcName = "John Doe";
            string CcEmail = "example3@example2.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);
            string HtmlContent = "<html><body><h1>This is my first transactional email {{params.parameter}}</h1></body></html>";
            string TextContent = null;
            string Subject = "My {{params.subject}}";
            string ReplyToName = "John Doe";
            string ReplyToEmail = "replyto@domain.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);
            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);
            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = null;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "New Subject");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, HtmlContent, TextContent, Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            //var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("szymon.fiedorowicz98@gmail.com", "byrzytdwtztssnum"),
            //    EnableSsl = true,
            //    UseDefaultCredentials = true,
            //};

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress("szymon.fiedorowicz98@gmail.com"),
            //    Subject = subject,
            //    Body = htmlMessage,
            //    IsBodyHtml = true,
            //};
            //mailMessage.To.Add(email);

            return System.Threading.Tasks.Task.CompletedTask;

            //return smtpClient.SendMailAsync("EventManagerStudent@engineer.com", email, subject, htmlMessage);

            //var message = new PostmarkMessage()
            //{
            //    To = email,
            //    From = "szymon.fiedorowicz@witkac.pl",
            //    Subject = subject,
            //    HtmlBody = htmlMessage,
            //};

            //var client = new PostmarkClient("84b63a08-aa29-413c-8a01-ae6a01dee7cf");

            //return client.SendMessageAsync(message);
        }
    }
}
