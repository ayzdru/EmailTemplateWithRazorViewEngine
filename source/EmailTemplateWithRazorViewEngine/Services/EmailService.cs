using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateWithRazorViewEngine.AppSettings;
using EmailTemplateWithRazorViewEngine.Interfaces;
using Hangfire;
using MimeKit;
using MimeKit.Text;

namespace EmailTemplateWithRazorViewEngine.Services
{
    public class EmailService : IEmailService
    {
        private readonly IViewRenderService _viewRenderService;
        private readonly EmailSettings _emailSettings;

        public EmailService(IViewRenderService viewRenderService, EmailSettings emailSettings)
        {
            _viewRenderService = viewRenderService;
            _emailSettings = emailSettings;
        }

        public string GetPartialViewHtml(string partialViewName, object model)
        {
            return _viewRenderService.RenderToStringAsync(partialViewName, model).Result;
        }
        public void Send(string email, string subject, string body)
        {
            try
            {
                var messageToSend = new MimeMessage
                {
                    Subject = subject
                };
                messageToSend.To.Add(new MailboxAddress(email));
                messageToSend.From.Add(new MailboxAddress(_emailSettings.Name, _emailSettings.Email));
                messageToSend.Body = new TextPart(TextFormat.Html) { Text = body };
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    //smtp.MessageSent += (sender, args) => {  };
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSsl);
                    smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                    smtp.Send(messageToSend);
                    smtp.Disconnect(true);

                }
            }
            catch (Exception ex)
            {


            }
        }
        public void SendEmail(string email, string subject, string body)
        {
            BackgroundJob.Enqueue(() => Send(email, subject, body));
        }
    }
}
