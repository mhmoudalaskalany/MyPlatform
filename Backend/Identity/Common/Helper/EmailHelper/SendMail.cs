using System;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Common.Helper.EmailHelper
{
    public class SendMail : ISendMail
    {
        private readonly EmailMetadata _notificationMetadata;
        private readonly ILogger<SendMail> _logger;
        public SendMail(EmailMetadata notificationMetadata, ILogger<SendMail> logger)
        {
            _notificationMetadata = notificationMetadata;
            _logger = logger;
        }
        //192.168.14.19
        public void Send(string mailTo, string body, string subject, bool supportHtml = false)
        {
            try
            {
                var message = new EmailMessage
                {
                    Sender = new MailboxAddress(_notificationMetadata.SenderName, _notificationMetadata.Sender),
                    Receiver = new MailboxAddress(mailTo, mailTo),
                    Subject = subject,
                    Content = body
                };
                var mimeMessage = CreateEmailMessage(message, supportHtml);
                var enableSsl = bool.Parse(_notificationMetadata.EnableSsl);
                using var smtpClient = new SmtpClient();
                smtpClient.Connect(_notificationMetadata.SmtpServer, _notificationMetadata.Port, enableSsl);
                smtpClient.Authenticate(_notificationMetadata.UserName, _notificationMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception Message At Send Mail :  {e.Message}");
            }

        }
        private MimeMessage CreateEmailMessage(EmailMessage message, bool supportHtml)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Receiver);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(supportHtml ? MimeKit.Text.TextFormat.Html : MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return mimeMessage;
        }
    }
}
